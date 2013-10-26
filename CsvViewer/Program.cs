using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvViewer
{
    class Program
    {
        private const string DefaultDataFile = "persons.csv";
        static DataFormatter _formatter;

        static void Main(string[] args)
        {
            _formatter = CreateFormatter(args);

            WriteTable(1);
            KeypressLoop();
        }

        private static string GetFileName(IList<string> args)
        {
            var file = DefaultDataFile;

            if (args.Any())
            {
                file = args[0];
            }

            return file;
        }

        private static DataFormatter CreateFormatter(string[] args)
        {
            var dataSource = new CsvFileDataSource(GetFileName(args));
            return new DataFormatter(new Data(dataSource));
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
