using System;

namespace JenkinCall
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            IList<string> cookies = new List<string>();
            CrumbDetail crumbObj;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic aaa=");

                var getResponse = httpClient.GetAsync(new Uri("https://example.com/api/json")).Result;
                var cookie = getResponse.Headers.GetValues("Set-Cookie");
                foreach (var c in cookie)
                {
                    cookies.Add(c);
                }

                var body = getResponse.Content.ReadAsStringAsync().Result;
                crumbObj = JsonConvert.DeserializeObject<CrumbDetail>(body);

            }

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic aaa=");
                httpClient.DefaultRequestHeaders.Add(crumbObj.crumbRequestField, crumbObj.crumb);
               
                var cookieValue = string.Join(";", cookies);
                var splits = cookieValue.Split(new[] { ';' });

                
                //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", splits[0]);
                           
                
                var keyValue = splits[0].Split(new[] { '=' });
                cookieContainer.Add(new Uri("https://example.com"),
                    new Cookie(keyValue[0], keyValue[1]));

                var getResponse = httpClient.PostAsync(new Uri("https://example.com/generateNewToken/api/json"), null).Result;

                var body = getResponse.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<TokenResponse>(body);
                Console.WriteLine("Token name :" + token.data.tokenName);
                Console.WriteLine("Token value :" + token.data.tokenValue);

            }



            Console.ReadKey();

        }

        public class CrumbDetail
        {
            public string _class { get; set; }
            public string crumb { get; set; }
            public string crumbRequestField { get; set; }
        }

        public class Data
        {
            public string tokenName { get; set; }
            public string tokenUuid { get; set; }
            public string tokenValue { get; set; }
        }

        public class TokenResponse
        {
            public string status { get; set; }
            public Data data { get; set; }
        }
    }
}
