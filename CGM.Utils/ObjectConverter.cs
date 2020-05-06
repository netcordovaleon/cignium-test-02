using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Utils
{
    public static class ObjectConverter
    {
        public static T GetObject<T>(Stream responseStream) where T : class
        {
            return JsonConvert.DeserializeObject<T>(new StreamReader(responseStream, Encoding.UTF8).ReadToEnd());
        }
    }
}
