using System.Collections.Generic;

namespace GenerateWord
{
    public class DynamicTable
    {
        public DynamicTable()
        {
            Rows = new List<Row>();
        }

        public string TableName { get; set; }

        public TypeIntegration TypeIntegration { get; set; }

        public List<Row> Rows { get; set; }
    }
}
