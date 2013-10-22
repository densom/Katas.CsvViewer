using System.Collections.Generic;

namespace CsvViewer
{
    public interface IDataSource
    {
        IEnumerable<string> GetData();
    }
}