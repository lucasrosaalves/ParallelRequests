using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace ParallelRequests
{
    public static class RequestParameters
    {
        public static HttpMethod HttpMethod = HttpMethod.Post;
        public static string Url = BuildUrl();
        public static List<int> Users = new List<int>() { 1, 10, 30, 50, 100 };
        public static string Body => ReadJson();
        public static string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCIsImtpZCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJodHRwczovL2NoYW1waW9ueC5taWNyb3NvZnRvbmxpbmUuY29tL29mYy13ZWJhcGktYXV0aGVudGljYXRpb24tMDAxLXEiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9mNDMwNTNiMi1lOTExLTQ4NDctYjgyNS01MjEwNTg0YzQxOGEvIiwiaWF0IjoxNjAxOTg4MTU3LCJuYmYiOjE2MDE5ODgxNTcsImV4cCI6MTYwMTk5MjA1NywiYWNyIjoiMSIsImFpbyI6IkFVUUF1LzhSQUFBQXNPTFhJYURTOVNPTVpHRkpkL0VxQ1p5eUhOWm9seVdXaW1vcjhmZGdqS2FoSE5Wd0ZIVTRyU2RJdkVSWGgxVm5WM3ZWRFlNTmdMdkZuaDZxZ3J0K1JRPT0iLCJhbXIiOlsicHdkIiwibWZhIl0sImFwcGlkIjoiOGU0ZWQ1YTAtYjkxMC00ZTRiLWE4YTQtNzdlZjgyN2Y0NWY1IiwiYXBwaWRhY3IiOiIwIiwiZmFtaWx5X25hbWUiOiJBbHZlcyIsImdpdmVuX25hbWUiOiJMdWNhcyIsImlwYWRkciI6IjE4Ny4xMDguMTc0Ljk4IiwibmFtZSI6IkFsdmVzLCBMdWNhcyIsIm9pZCI6Ijk3ZTBkMDliLWY5YTctNDRiMi05OGViLTkzMjRjNzAzODk5NCIsIm9ucHJlbV9zaWQiOiJTLTEtNS0yMS0zMDgzNDUwMzkxLTE1NzQwNjczOTQtMzgwMTE0ODk2MC0xOTUyNyIsInJoIjoiMC5BVFlBc2xNdzlCSHBSMGk0SlZJUVdFeEJpcURWVG80UXVVdE9xS1IzNzRKX1JmVTJBSm8uIiwic2NwIjoidXNlcl9pbXBlcnNvbmF0aW9uIiwic3ViIjoid1llM2t1NnZnTVlLU01Vc2dLa1NxTk9TcEROdjNUTG1xOUZuNDVrQlU3cyIsInRpZCI6ImY0MzA1M2IyLWU5MTEtNDg0Ny1iODI1LTUyMTA1ODRjNDE4YSIsInVuaXF1ZV9uYW1lIjoibHVjYXMuYWx2ZXNAY2hhbXBpb254LmNvbSIsInVwbiI6Imx1Y2FzLmFsdmVzQGNoYW1waW9ueC5jb20iLCJ1dGkiOiJseGczREJCUEVrZVBlNzJmX3prS0FnIiwidmVyIjoiMS4wIn0.W4_pn4z6llqngRf601N1ik-bjOOwPLRlvfABB5KDyuk_PXRd_pke6mn6jCJJNKRE_H5ocMWovUHrxiTKv1p8lYhC-ymIsNJAYKa2awTLpL0Ysdzq-MVdkGz68WWk0yeVXAQ4jeXbO8iWJSQQx8v2z5hibwULsNdt45pzYiaI_PFPBZDJm9AKwPw_Dhu3IGifuz85E7stpSYjXCHQvgW3-YcvCJdGBJY0E5xTav_3N1gu1cGqNOk_fEvOsHn5GojL_DndLENys5k2aV67UPEO1a4C010vZRb5Ixdh9QHjRtfmiC6lBT5YtU5lCGGF4eb6oJELOiyhVo4zYqZsxPyPVQ";
        public static Dictionary<string, string> Headers = new Dictionary<string, string>()
        {
             { "authorizationHash", "6f8qNSGeg9ZqPt5i1QOJdEi+CHQy0RngdJDPkPWL9MuoYjevV7Q6IsLMIoV8MW6OU5U1LWcQEqF3DTeGUKzdePo0BfPlrtPJkwtEJfChb901w0OOqi7tTQwq1AznF4VraFt7XOkoSUT+2zn1syt+kfuS8OW1gBipGa1IlZ8bjpATyDn0yjo5kKlUeAyt4E2yzwRUpYSdOUzOqN8szuKOkwXMmrNtt7dWExgmPEmtntY01EvjengyIODaX9BKfdqZ" }
        };

        private static string ReadJson()
        {
            using (StreamReader r = new StreamReader("body.json"))
            {
                return r.ReadToEnd();
            }
        }

        private static string BuildUrl()
        {
            var baseUrl = "https://ofc-customerportalpersistence-api-001-q.azurewebsites.net/api/";
            var endpoint = "UserToUom/SavePreferredUoms/1CDE5B80-DF53-4C3F-AFC3-A71231F86A05/ac34fd20-e1c0-49f8-8728-526fa48a671c";

            return string.Concat(baseUrl, endpoint);
        }
    }
}
