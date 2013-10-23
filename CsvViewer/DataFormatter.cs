using System.Collections.Generic;
using System.Linq;

namespace CsvViewer
{
    public class DataFormatter
    {
        public DataFormatter(Data data)
        {
            Data = data;
        }

        public Data Data { get; private set; }

        public string GetHeaderString()
        {
            var paddedHeaders = new List<string>();

            int column = 0;
            foreach (var header in Data.Header)
            {
                var longestColumn = Data.Rows.Count() == 0 ? 0 : Data.Rows.Select(r => r[column]).Max(f => f.Length);
                paddedHeaders.Add(header.PadRight(longestColumn, ' '));
                column++;
            }

            return string.Join("|", paddedHeaders) + "|";
        }
    }
}