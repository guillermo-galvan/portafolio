using System.Collections.Generic;

namespace GenerateWord
{
    public class Row
    {
        public Row()
        {
            Columns = new Dictionary<int, string>();
        }

        public Dictionary<int, string> Columns { get; set; }
    }
}
