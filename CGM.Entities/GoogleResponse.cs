using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Entities
{
    public class GoogleResponse
    {
        public GoogleQuery queries { get; set; }
    }

    public class GoogleQuery
    {
        public List<GoogleRequest> request { get; set; }
    }

    public class GoogleRequest
    {
        public string title { get; set; }
        public long totalResults { get; set; }
    }
}
