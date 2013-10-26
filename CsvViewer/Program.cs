using System;
using System.Linq;

namespace CsvViewer
{
    class Program
    {
        private static IDataSource _dataSource;
        static DataFormatter _formatter;

        static void Main(string[] args)
        {
            SetDataSource(args);

            WriteTable(1);
            KeypressLoop();
        }

        private static void SetDataSource(string[] args)
        {
            var file = "persons.csv";

            if (args.Any())
            {
                file = args[0];
            }

            _formatter = CreateFormatter(file);
        }

        private static DataFormatter CreateFormatter(string file)
        {
            _dataSource = new CsvFileDataSource(file);
            return new DataFormatter(new Data(_dataSource));
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
                        currentPage = _formatter.Data.PageCount;
                        break;
                }

                WriteTable(currentPage);

            } while (keypress != ConsoleKey.X);
        }

        private static void WriteTable(int page)
        {
            Console.Clear();
            Console.WriteLine(_formatter.GetTable(page));
            Console.WriteLine();
            Console.WriteLine("N(ext page, P(revious page, F(irst page, L(ast page, eX(it");
        }
    }
}
