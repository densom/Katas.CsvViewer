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

            for (int i = 0; i < Data.Header.Count(); i++)
            {
                paddedHeaders.Add(Data.Header.ElementAt(i).PadRight(GetLongestColumns(page).ElementAt(i), ' '));
            }


            return string.Join("|", paddedHeaders) + "|";
        }

        private IEnumerable<int> GetLongestColumns(int page = 1)
        {
            for (var i = 0; i < Data.Header.Count(); i++)
            {
                var longestColumn = Data.GetPageWithHeaders(page).Select(r => r[i]).Max(f => f.Length);
                yield return longestColumn;
            }
        }

        public string GetSeparatorString(int page = 1)
        {
            var dashList = new List<string>();

            for (var i = 0; i < Data.Header.Count(); i++)
            {
                dashList.Add(new string('-', GetLongestColumns(page).ElementAt(i)));
            }

            return string.Join("+", dashList) + "+";
        }

        public IEnumerable<string> GetRowStrings(int page = 1)
        {
            new List<string[]>();

            for (int i = 0; i < Data.Rows.Count(); i++)
            {
                var paddedRow = new List<string>();

                for (int j = 0; j < Data.Rows.ElementAt(i).Count(); j++)
                {
                    paddedRow.Add(Data.Rows.ElementAt(i).ElementAt(j).PadRight(GetLongestColumns(page).ElementAt(j), ' '));
                }

                yield return string.Join("|", paddedRow.ToArray()) + "|";

            }
        }
    }
}