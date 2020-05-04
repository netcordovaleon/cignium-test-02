using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Factory
{
    public class BingSearchEngineFactory : SearchEngineFactory
    {
        public override SearchEngine GetSearchEngine()
        {
            return new BingSearchEngine();
        }
    }
}
