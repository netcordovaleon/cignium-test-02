using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Factory
{
    public abstract class SearchEngineFactory
    {
        public abstract SearchEngine GetSearchEngine();
    }
}
