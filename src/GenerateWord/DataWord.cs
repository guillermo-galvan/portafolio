using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateWord
{
    public class DataWord
    {
        public Dictionary<string, string> KeyValues { get; set; }

        public string PathTemplate { get; set; }

        public string Password { get; set; }

        public string PathFileFinaly { get; set; }

        public string PathEmbeddedPackageParts { get; set; }

        public List<DynamicTable> DynamicTable { get; set; }
    }
}
