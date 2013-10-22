using System.Collections.Generic;

namespace CsvViewer
{
    public class Data
    {
        private string[] _rows;

        public void SetHeader(params string[] headerValues)
        {
            Header = headerValues;
        }

        public string[] Header { get; private set; }

        public IEnumerable<string[]> Rows
        {
            get
            {
                return new List<string[]> {_rows};
            }
        }

        public void AddRow(params string[] rowValues)
        {
            _rows = rowValues;
        }
    }
}