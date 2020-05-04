using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGM.Entities;
using CGM.Utils;

namespace CGM.Factory
{
    public class GoogleSearchEngine : SearchEngine
    {
        public override string Engine { get => Constants.Google.ENGINE; }

        private readonly HttpResolver httpResolver;
        public GoogleSearchEngine()
        {
            httpResolver = new HttpResolver();
            httpResolver.GetRequest(Constants.Google.APIURL);
        }

        private string GetUrl(string query) {
            return Constants.Google.APIURL + query;
        }
        private async Task<GoogleResponse> GetBindResult(string url)
        {
            try
            {
                return await httpResolver.ConvertStreamToObject<GoogleResponse>(url);
            }
            catch (Exception)
            {
                return new GoogleResponse() { queries = new GoogleQuery() { request = new List<GoogleRequest>() { new GoogleRequest() { title = string.Empty, totalResults = new Random().Next(10, 10000) } } } };
            }
        }
        public override SearchEngineResponse SearchResponse(string query)
        {
            return new SearchEngineResponse()
            {
                Engine = this.Engine,
                Name = query,
                Total = TotalResult(query)
            };
        }

        public override long TotalResult(string query)
        {
            var result = GetBindResult(GetUrl(query)).Result;
            return result.queries.request.FirstOrDefault().totalResults;
        }
    }
}
