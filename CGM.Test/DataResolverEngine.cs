using CGM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGM.Utils;
namespace CGM.Test
{
    public  class DataResolverEngine
    {
        static DataResolverEngine _instance = null;
        public DataResolverEngine()
        {
        }
        public static DataResolverEngine GetInstance() {
           return (_instance == null) ? new DataResolverEngine() : _instance;
        }
        public IList<SearchEngineResponse> Data()
        {
            var response = new List<SearchEngineResponse>();
            response.Add(new SearchEngineResponse() { Name = "java", Engine = Constants.Google.ENGINE, Total = 1000 });
            response.Add(new SearchEngineResponse() { Name = "net", Engine = Constants.Google.ENGINE, Total = 50000 });
            response.Add(new SearchEngineResponse() { Name = "javascript", Engine = Constants.Google.ENGINE, Total = 2500 });
            response.Add(new SearchEngineResponse() { Name = "java", Engine = Constants.Bing.ENGINE, Total = 500 });
            response.Add(new SearchEngineResponse() { Name = "net", Engine = Constants.Bing.ENGINE, Total = 26000 });
            response.Add(new SearchEngineResponse() { Name = "javascript", Engine = Constants.Bing.ENGINE, Total = 26001 });
            return response;
        }
    }
}
