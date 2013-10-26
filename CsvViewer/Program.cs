using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvViewer
{
    class Program
    {
        static readonly IDataSource DataSource = new CsvFileDataSource("persons.csv");
        static readonly DataFormatter Formatter = new DataFormatter(new Data(DataSource));

        static void Main(string[] args)
        {
            WriteTable(1);
            KeypressLoop();
        }

        private static void KeypressLoop()
        {
            ConsoleKey keypress;
            var currentPage = 1;

            do
            {
                keypress = Console.ReadKey().Key;

                switch (keypress)
                {
                    case ConsoleKey.N:
                        currentPage++;
                        break;
                    case ConsoleKey.P:
                        currentPage--;
                        break;
                    case ConsoleKey.F:
                        currentPage = 1;
                        break;
                    case ConsoleKey.L:
                        throw new NotImplementedException("need to implement page count");
                }

                WriteTable(currentPage);

            } while (keypress != ConsoleKey.X);
        }

        private static void WriteTable(int page)
        {
            Console.Clear();
            Console.WriteLine(Formatter.GetTable(page));
            Console.WriteLine();
            Console.WriteLine("N(ext page, P(revious page, F(irst page, L(ast page, eX(it");
        }
    }
}
