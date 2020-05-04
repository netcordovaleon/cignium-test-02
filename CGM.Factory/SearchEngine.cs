using CGM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Factory
{
    public abstract class SearchEngine
    {
        public abstract string Engine { get; }
        public abstract long TotalResult(string query);
        public abstract SearchEngineResponse SearchResponse(string query);
    }
}
