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

        public string GetHeaderString(int page = 1)
        {
            var paddedHeaders = new List<string>();

            int column = 0;
            foreach (var header in Data.Header)
            {
                paddedHeaders.Add(header.PadRight(GetLongestColumns().ElementAt(column), ' '));
                column++;
            }

            return string.Join("|", paddedHeaders) + "|";
        }

        private IEnumerable<int> GetLongestColumns(int page = 1)
        {
            int column = 0;
            foreach (var header in Data.Header)
            {
                var longestColumn = Data.Rows.Count() == 0 ? 0 : Data.GetPage(page).Select(r => r[column]).Max(f => f.Length);
                yield return longestColumn;
                column++;
            }
        }
    }
}