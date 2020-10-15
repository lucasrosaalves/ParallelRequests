using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ParallelRequests
{
    public class RequestParameters
    {
        public HttpMethod HttpMethod { get; }
        public List<int> Users { get; }
        public string Token { get; }
        public string Url { get; }
        public string Body { get; }
        public Dictionary<string, string> Headers { get; }

        public RequestParameters()
        {
            Url = BuildUrl();
            Body = GetBody();
            Headers = GetHeaders();
            HttpMethod = HttpMethod.Post;
            Users = new List<int>() { 1, 10 ,30 ,50, 100 };
            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCIsImtpZCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJodHRwczovL2NoYW1waW9ueC5taWNyb3NvZnRvbmxpbmUuY29tL29mYy13ZWJhcGktYXV0aGVudGljYXRpb24tMDAxLXEiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mNDMwNTNiMi1lOTExLTQ4NDctYjgyNS01MjEwNTg0YzQxOGEvIiwiaWF0IjoxNjAyNzY2MDQ1LCJuYmYiOjE2MDI3NjYwNDUsImV4cCI6MTYwMjc2OTk0NSwiYWNyIjoiMSIsImFpbyI6IkFVUUF1LzhSQUFBQTlOZzNNRlUvOVNOWXI5eENTSzlJYlJDQ2ViMDJVNmM4MW0wWWNpZzI4S0dZOWtyT29PYTlUOG90NUZRdUxmWkc2SHJ1QXNkNWFkUWNDK0J4akpHc0lRPT0iLCJhbXIiOlsicHdkIiwibWZhIl0sImFwcGlkIjoiOGU0ZWQ1YTAtYjkxMC00ZTRiLWE4YTQtNzdlZjgyN2Y0NWY1IiwiYXBwaWRhY3IiOiIwIiwiZmFtaWx5X25hbWUiOiJBbHZlcyIsImdpdmVuX25hbWUiOiJMdWNhcyIsImlwYWRkciI6IjIwMS4xNy45MC4xNzUiLCJuYW1lIjoiQWx2ZXMsIEx1Y2FzIiwib2lkIjoiOTdlMGQwOWItZjlhNy00NGIyLTk4ZWItOTMyNGM3MDM4OTk0Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTMwODM0NTAzOTEtMTU3NDA2NzM5NC0zODAxMTQ4OTYwLTE5NTI3IiwicmgiOiIwLkFUWUFzbE13OUJIcFIwaTRKVklRV0V4QmlxRFZUbzRRdVV0T3FLUjM3NEpfUmZVMkFKby4iLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJ3WWUza3U2dmdNWUtTTVVzZ0trU3FOT1NwRE52M1RMbXE5Rm40NWtCVTdzIiwidGlkIjoiZjQzMDUzYjItZTkxMS00ODQ3LWI4MjUtNTIxMDU4NGM0MThhIiwidW5pcXVlX25hbWUiOiJsdWNhcy5hbHZlc0BjaGFtcGlvbnguY29tIiwidXBuIjoibHVjYXMuYWx2ZXNAY2hhbXBpb254LmNvbSIsInV0aSI6InJOV0w3NzBjQmtpR1R1Tk1GSFFUQUEiLCJ2ZXIiOiIxLjAifQ.SgNe5ClE4nIGT1ffDZ2U9eebdVwnrysTu_1Tewa_vpf14LTft5f6UgJxwFOpVCZDcwDuZX3yVMGw1Uib5hcLuXRcAFoTBWO69yyzQixyR4g8TkHdPJLG80HmPDsW9-eWfqPy0mJzw2siF1xM1Sx6Knlu4k2DwkzaXKCbmGo28Nce7Qi1URy9DmvkiIswbvXBjfp0Z9JsBPmcdf0xwKa1hnNeswmPJ4Y8S356CeWmM-9Hc0fTIsv-GVBG_dYkNYH2HHWEeAN60kTDXHeWWOb-PUcTCWgxe933UuG9jBVRaTBQPHuWVhcM3eVVfSjZvd9t9hWA8RryzpUl-4NTrycLYA";
        }

        private string GetBody()
        {
            using (StreamReader r = new StreamReader("body.json"))
            {
                return r.ReadToEnd();
            }
        }

        private Dictionary<string, string> GetHeaders()
        {
            var result = new Dictionary<string, string>()
            {
                {"siteId", "f4337c75-6729-4b6a-9cb6-560b2c48d4c1" },
                { "authorizationHash", "6f8qNSGeg9ZqPt5i1QOJdEi+CHQy0RngdJDPkPWL9Muk7KkXL6jG2YpO9QNTVcypn/NzTRG/cVYDCLPtBdn2HywtjgMRq4uUvsygtpJNCSeioSZeLOyVgz8ZkqYOITV9KIVME7lXHI6CE99pxbXy133zpe9kOpJR2PJtODmz+1x8GoVt61TCp5RKbji33gvoq+27wZOHVlbbIyD078IsJpRC4vC1cMd+6Wp6WQsWIbXmO5ZxtbVq8NxOk4LJquu5" }
            };

            result.Add("User-Preferred-Uoms", ReadUserUoms().Replace(Environment.NewLine, ""));
            return result;
        }

        public string ReadUserUoms()
        {
            using (StreamReader r = new StreamReader("user_uoms.json", Encoding.ASCII))
            {
                return r.ReadToEnd();
            }
        }

        private string BuildUrl()
        {
            var baseUrl = "http://localhost:61604/api/";
            var endpoint = "Dashboard/CompletedDeliveriesTruckTreatment?search=";

            return string.Concat(baseUrl, endpoint);
        }
    }
}
