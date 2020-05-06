using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Utils
{
    public class CGMException : Exception
    {
        public CGMException()
        {
        }
        public CGMException(string message) : base(message)
        {
        }

        public CGMException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
