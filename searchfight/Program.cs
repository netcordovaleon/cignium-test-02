using CGM.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchfight
{
    class Program
    {
        static void Main(string[] args)
        {
            ResolverSearchEngine engine = new ResolverSearchEngine();
            engine.MainSearchEngine(args);
        }
    }
}
