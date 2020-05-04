using CGM.Entities;
using CGM.Factory;
using CGM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Resolver
{
    public class ResolverSearchEngine
    {
        private readonly string[] searchEngines;
        public ResolverSearchEngine()
        {
            this.searchEngines = new string[] { Constants.Google.ENGINE, Constants.Bing.ENGINE };
        }

        public void MainSearchEngine(string[] arg) {
            Console.WriteLine(SearchEngineResultInString(arg));
            Console.ReadKey();
        }

        public string SearchEngineResultInString(string[] arg) {
            string response = string.Empty;
            if (arg.Length < 2)
                throw new ArgumentException(Constants.General.DONTHAVEWORLDSFORSEARCH);
            response = GetFinalSearchResult(arg);
            return response;
        }

        public long GetTotalResultByEngineAndLanguage(IList<SearchEngineResponse> response, string Engine, string lang) {
            return response.FirstOrDefault(c => c.Engine == Engine && c.Name == lang).Total;
        }
        public SearchEngineResponse GetWinnerByEngine(IList<SearchEngineResponse> response, string Engine)
        {
            return response.Where(x => x.Engine == Engine).OrderByDescending(x => x.Total).FirstOrDefault();
        }

        public SearchEngineResponse GetWinner(IList<SearchEngineResponse> response)
        {
            var groupResponseByLanguages = response.GroupBy(item => item.Name);
            return groupResponseByLanguages.Select(x => new SearchEngineResponse() { Name = x.Key, Total = x.Sum(y => y.Total) }).OrderByDescending(z => z.Total).First();
        }

        private string GetSummarySearchResult(string[] languages, IList<SearchEngineResponse> response) {
            StringBuilder str = new StringBuilder();
            foreach (var lang in languages)
            {
                var inGoogle = GetTotalResultByEngineAndLanguage(response, Constants.Google.ENGINE, lang);
                var inBing = GetTotalResultByEngineAndLanguage(response, Constants.Bing.ENGINE, lang);
                str.Append($"{lang} Google Search: { inGoogle } Bing Search: { inBing } \n");
            }
            return str.ToString();
        }
        
        private string GetFinalSearchResult(string[] languages) {
            StringBuilder result = new StringBuilder();
            var getListSearchResult = GetListSearchResult(languages);
            result.Append(GetSummarySearchResult(languages, getListSearchResult));
            result.Append($"Google Winner: { GetWinnerByEngine(getListSearchResult, Constants.Google.ENGINE).Name } \n");
            result.Append($"Bing Winner: { GetWinnerByEngine(getListSearchResult, Constants.Bing.ENGINE).Name } \n");
            result.Append($"TOTAL Winner: { GetWinner(getListSearchResult).Name } \n");
            return result.ToString();
        } 

        private IList<SearchEngineResponse> GetListSearchResult(string[] languages) {
            IList<SearchEngineResponse> listSearch = new List<SearchEngineResponse>();
            foreach (var lan in languages)
            {
                foreach (var engine in searchEngines)
                {
                    var searchEngine = GetFactoryInstance(engine).GetSearchEngine();
                    listSearch.Add(searchEngine.SearchResponse(lan));
                }
            }
            return listSearch;
        }

        private SearchEngineFactory GetFactoryInstance(string engine) {

            SearchEngineFactory factory = null;
            if (engine == Constants.Google.ENGINE)
                factory = new GoogleSearchEngineFactory();
            else
                factory = new BingSearchEngineFactory();
            return factory;
        }

    }
}
