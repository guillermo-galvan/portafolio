using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace GenerateWord
{
    internal class DocumentDetail
    {
        public Paragraph Paragraph { get; set; }

        public List<Run> Runs { get; set; }

        public List<(int Index, string StringComlemento)> Indexs { get; set; }

        public string Content { get; set; }
    }
}
