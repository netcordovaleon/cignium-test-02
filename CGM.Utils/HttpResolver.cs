using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Utils
{
    public class HttpResolver
    {
        public HttpWebRequest GetRequest(string url, string token = "")
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            if (!string.IsNullOrEmpty(token))
            {
                httpRequest.Headers["Ocp-Apim-Subscription-Key"] = token;
            }
            return httpRequest;
        }

        public async Task<T> ConvertStreamToObject<T>(string url) where T : class
        {
            using (Stream responseStream = await GetResponseAsStream(url))
                return ObjectConverter.GetObject<T>(responseStream);
        }
        private async Task<WebResponse> GetWebResponse(string url)
        {
            return await GetRequest(url).GetResponseAsync();
        }

        private async Task<Stream> GetResponseAsStream(string url)
        {
            return (await GetWebResponse(url)).GetResponseStream();
        }
    }
}
