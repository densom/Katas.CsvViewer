using System.Collections.Generic;

namespace CsvViewer
{
    public class Data
    {
        private List<string[]> _rows = new List<string[]>();

        public void SetHeader(params string[] headerValues)
        {
            Header = headerValues;
        }

        public string[] Header { get; private set; }

        public IEnumerable<string[]> Rows
        {
            get { return _rows; }
        }

        public void AddRow(params string[] rowValues)
        {
            _rows.Add(rowValues);
        }
    }
}