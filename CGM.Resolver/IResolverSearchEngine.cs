using CGM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Resolver
{
    public interface IResolverSearchEngine
    {
        string SearchEngineResultInString(string[] arg);
        void MainSearchEngine(string[] arg);
        long GetTotalResultByEngineAndLanguage(IList<SearchEngineResponse> response, string Engine, string lang);
        SearchEngineResponse GetWinnerByEngine(IList<SearchEngineResponse> response, string Engine);
        SearchEngineResponse GetWinner(IList<SearchEngineResponse> response);
    }
}
