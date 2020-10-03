using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ParallelRequests
{
    public class Client : IClient
    {
        private HttpClient _httpClient;

        public Client(HttpClient httpClient)
        {
            _httpClient = httpClient ??
                throw new ArgumentException(nameof(httpClient));
        }

        public async Task Handle()
        {
            var results = new List<RequestResult>();

            foreach (var users in RequestParameters.Users)
            {
                var result = await Handle(users);

                results.Add(result);
                Console.WriteLine(result.ToString());
            }

            PrintResults(results);
        }

        private void PrintResults(List<RequestResult> results)
        {
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(RequestParameters.Url);
            foreach (var result in results)
            {
                Console.WriteLine(result.ToString());
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private async Task<RequestResult> Handle(int users)
        {
            List<Task<bool>> tasks = BuildTasks(users);

            var timer = new Stopwatch();
            timer.Start();

            var result = await Task.WhenAll(tasks);

            timer.Stop();

            return new RequestResult()
            {
                Requests = users,
                TotalSeconds = timer.Elapsed.TotalSeconds,
                SuccessfulRequests = result.Count(p => p)
            };
        }

        private List<Task<bool>> BuildTasks(int users)
        {
            var tasks = new List<Task<bool>>();
            for (int i = 0; i < users; i++)
            {
                tasks.Add(SendAsync());
            }

            return tasks;
        }

        private async Task<bool> SendAsync()
        {
            var timer = new Stopwatch();
            timer.Start();

            HttpRequestMessage request = CreateRequest();

            var response = await _httpClient.SendAsync(request);

            timer.Stop();

            Console.WriteLine($"{timer.Elapsed.TotalSeconds}s elapsed with response {response.StatusCode}");

            return response.IsSuccessStatusCode;
        }


        #region Create Request

        private static HttpRequestMessage CreateRequest()
        {
            var request = new HttpRequestMessage(RequestParameters.HttpMethod, RequestParameters.Url);

            AddToken(request);
            AddHeaders(request);
            AddContent(request);

            return request;
        }

        private static void AddContent(HttpRequestMessage request)
        {
            if (RequestParameters.Body != null)
            {
                request.Content = new StringContent(RequestParameters.Body.ToString(), Encoding.UTF8, "application/json");
            }
        }

        private static void AddToken(HttpRequestMessage request)
        {
            if (!string.IsNullOrWhiteSpace(RequestParameters.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", RequestParameters.Token);
            }
        }

        private static void AddHeaders(HttpRequestMessage request)
        {
            if (RequestParameters.Headers != null)
            {
                foreach (var header in RequestParameters.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        #endregion
    }
}
