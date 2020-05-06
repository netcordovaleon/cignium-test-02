using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Utils
{
    public static class Constants
    {
        public static class General {
            public const string DONTHAVEWORLDSFORSEARCH = "You should write one languages";
        }
        public static class Google {
            public const string ENGINE = "Google";
            public static string APIURL
            {
                get => $"https://www.googleapis.com/customsearch/v1?key={ TOKEN }&cx=017576662512468239146:omuauf_lfve&q=";
            }
            public static string TOKEN
            {
                get => ConfigurationManager.AppSettings["KEY_GOOGLE"].ToString();
            }
        }

        public static class Bing {
            public const string ENGINE = "Bing";
            public static string APIURL
            {
                get => "https://api.cognitive.microsoft.com/bing/v7.0/search?q=";
            }
            public static string TOKEN
            {
                get => ConfigurationManager.AppSettings["KEY_BING"].ToString();
            }
        }
    }
}
