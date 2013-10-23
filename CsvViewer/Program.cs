using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataSource = new CsvFileDataSource("persons.csv");

            var formatter = new DataFormatter(new Data(dataSource));

            Console.WriteLine(formatter.GetHeaderString());
            Console.WriteLine(formatter.GetSeparatorString());
            formatter.GetRowStrings(1).ToList().ForEach(Console.WriteLine);

        }
    }
}
