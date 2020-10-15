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
        private readonly HttpClient _httpClient;
        private readonly RequestParameters _parameters;

        public Client(HttpClient httpClient)
        {
            _httpClient = httpClient ??
                throw new ArgumentException(nameof(httpClient));

            _parameters = new RequestParameters();
        }

        public async Task Handle()
        {
            var results = new List<RequestResult>();

            foreach (var users in _parameters.Users)
            {
                RequestResult result = await Handle(users);
                results.Add(result);
                Console.WriteLine(result.ToString());
            }

            PrintResults(results);
        }

        private void PrintResults(List<RequestResult> results)
        {
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(_parameters.Url);
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
            try
            {
                var timer = new Stopwatch();
                timer.Start();

                HttpRequestMessage request = CreateRequest();

                var response = await _httpClient.SendAsync(request);

                timer.Stop();

                Console.WriteLine($"{timer.Elapsed.TotalSeconds}s elapsed with response {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #region Create Request

        private HttpRequestMessage CreateRequest()
        {
            var request = new HttpRequestMessage(_parameters.HttpMethod, _parameters.Url);

            AddToken(request);
            AddHeaders(request);
            AddContent(request);

            return request;
        }

        private void AddContent(HttpRequestMessage request)
        {
            if (!string.IsNullOrWhiteSpace(_parameters.Body))
            {
                request.Content = new StringContent(_parameters.Body, Encoding.UTF8, "application/json");
            }
        }

        private void AddToken(HttpRequestMessage request)
        {
            if (!string.IsNullOrWhiteSpace(_parameters.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _parameters.Token);
            }
        }

        private void AddHeaders(HttpRequestMessage request)
        {
            if (_parameters.Headers != null)
            {
                foreach (var header in _parameters.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        #endregion
    }
}
