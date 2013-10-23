using System.Collections.Generic;

namespace CsvViewer
{
    public class CsvFileDataSource : IDataSource
    {

        public string File { get; private set; }

        public CsvFileDataSource(string file)
        {
            File = file;
        }

        public IEnumerable<string> GetData()
        {
            using (var reader = new System.IO.StreamReader(File))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
                
            }
        }
    }
}