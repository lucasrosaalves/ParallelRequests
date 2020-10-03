using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace ParallelRequests
{
    public static class RequestParameters
    {
        private static string _baseUrl = "https://localhost:5014/api/";
        private static string _endpoint = "Deliveries/GetMatchedDeliveries/f4337c75-6729-4b6a-9cb6-560b2c48d4c1?startDate=2018-06-01&endDate=2019-01-01";
        private static object _bodyObject = new
        {
            AssetsId = new[] { "2921c604-fcaa-4067-8a82-1666d1508250" },
            DeliveryTypes = new[] { "Tank Usage Rate" }
        };


        public static string Url = string.Concat(_baseUrl, _endpoint);
        public static List<int> Users = new List<int>() { 1,10,30,50,100 };
        public static HttpMethod HttpMethod = HttpMethod.Post;
        public static JObject Body => JObject.FromObject(_bodyObject);
        public static string Token = "";
        public static Dictionary<string, string> Headers = new Dictionary<string, string>();
    }
}
