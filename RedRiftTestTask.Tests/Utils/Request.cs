using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RedRiftTestApplication;

namespace RedRiftTestTask.Tests.Utils
{
    public class Request
    {
        public static async Task<HttpResponseMessage> PerformRequestAsync(HttpMethod httpMethod, string endpoint, Dictionary<string, string> postParams = null)
        {
            IWebHostBuilder webHostBuilder = new WebHostBuilder().UseStartup<Startup>();
                        
            TestServer server = new TestServer(webHostBuilder);
            HttpClient client = server.CreateClient();
            string requestUrl = $"{client.BaseAddress}api/{endpoint}";
                        
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUrl)
            };
            
            if (httpMethod.Method == HttpMethod.Get.ToString())
            {
                requestMessage.Method = HttpMethod.Get;
            }
            else if (httpMethod.Method == HttpMethod.Post.ToString())
            {
                requestMessage.Method = HttpMethod.Post;

                if (postParams != null)
                {
                    string requestParams = string.Empty;
                    
                    foreach (var postParam in postParams)
                    {
                        requestParams += postParam.Value + "&";
                    }

                    requestParams = requestParams.Remove(requestParams.Length - 1);
                    requestMessage.Content = new StringContent(requestParams, Encoding.UTF8, "application/json");
                }
            }

            return await client.SendAsync(requestMessage);
        }
    }
}