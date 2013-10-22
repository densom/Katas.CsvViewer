﻿using System.Collections.Generic;
using System.Linq;

namespace CsvViewer
{
    public class CsvViewerData
    {
        private readonly char[] _delimiters = new[] {';'};

        public CsvViewerData(IDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public IDataSource DataSource { get; private set; }

        public string[] Header { get { return ParseHeader(); } }

        public IEnumerable<string[]> Rows
        {
            get { return ParseRows(); }
        }

        private IEnumerable<string[]> ParseRows()
        {
            return DataSource.GetData().Skip(1).Select(r => r.Split(_delimiters));
        }

        private string[] ParseHeader()
        {
            return DataSource.GetData().First().Split(_delimiters);
        }
    }
}