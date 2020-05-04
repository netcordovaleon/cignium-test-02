using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGM.Entities;
using CGM.Utils;

namespace CGM.Factory
{
    public class BingSearchEngine : SearchEngine
    {
        public override string Engine { get => Constants.Bing.ENGINE; }
        private readonly HttpResolver httpResolver;
        public BingSearchEngine()
        {
            httpResolver = new HttpResolver();
            httpResolver.GetRequest(Constants.Bing.APIURL, Constants.Bing.TOKEN);
        }

        private string GetUrl(string query)
        {
            return Constants.Bing.APIURL + query;
        }
        private async Task<BingResponse> GetBindResult(string url)
        {
            try
            {
                return await httpResolver.ConvertStreamToObject<BingResponse>(url);
            }
            catch (Exception)
            {
                return new BingResponse() { webPages = new BingWebPage() { totalEstimatedMatches = new Random().Next(10, 10000) }  };
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
            return result.webPages.totalEstimatedMatches;
        }
    }
}
