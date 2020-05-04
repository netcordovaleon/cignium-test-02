using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Entities
{
    public class SearchEngineResponse
    {
        public string Engine { get; set; }
        public string Name { get; set; }
        public long Total { get; set; }
        public SearchEngineResponse()
        {
        }
    }
}
