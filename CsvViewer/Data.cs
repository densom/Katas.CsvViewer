﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvViewer
{
    public class Data
    {
        private readonly char[] _delimiters = new[] { ';' };
        private readonly int _pageSize = 3;

        public Data(IDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public IDataSource DataSource { get; private set; }

        public string[] Header { get { return ParseHeader(); } }

        public IEnumerable<string[]> Rows
        {
            get { return ParseRows(); }
        }

        public int PageCount
        {
            get
            {
                var pages = Rows.Count() / _pageSize;
                if (Rows.Count() % _pageSize > 0)
                {
                    pages++;
                }
                return pages;
            }
        }

        private IEnumerable<string[]> ParseRows()
        {
            return DataSource.GetData().Skip(1).Select(r => r.Split(_delimiters));
        }

        private string[] ParseHeader()
        {
            return DataSource.GetData().First().Split(_delimiters);
        }

        public IEnumerable<string[]> GetPage(int pageNumber)
        {
            var rowsToSkip = (pageNumber - 1) * _pageSize;
            return Rows.Skip(rowsToSkip).Take(_pageSize);
        }

        public IEnumerable<string[]> GetPageWithHeaders(int pageNumber = 1)
        {
            yield return Header;

            foreach (var row in GetPage(pageNumber))
            {
                yield return row;
            }
        }
    }
}