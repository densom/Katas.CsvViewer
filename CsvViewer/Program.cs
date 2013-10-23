using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvViewer
{
    class Program
    {
        static IDataSource dataSource = new CsvFileDataSource("persons.csv");

        static DataFormatter formatter = new DataFormatter(new Data(dataSource));

        static void Main(string[] args)
        {




            ConsoleKey keypress;

            var currentPage = 1;

            WriteTable(currentPage);

            do
            {
                keypress = Console.ReadKey().Key;

                switch (keypress)
                {
                    case ConsoleKey.N:
                        WriteTable(++currentPage);
                        break;
                    case ConsoleKey.P:
                        WriteTable(--currentPage);
                        break;
                }



            } while (keypress != ConsoleKey.X);

        }

        private static void WriteTable(int page)
        {
            Console.Clear();
            Console.WriteLine(formatter.GetHeaderString(page));
            Console.WriteLine(formatter.GetSeparatorString(page));
            formatter.GetRowStrings(page).ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
            Console.WriteLine("N(ext page, P(revious page, F(irst page, L(ast page, eX(it");
        }
    }
}
