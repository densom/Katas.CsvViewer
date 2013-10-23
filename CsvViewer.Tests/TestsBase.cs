using System.Collections.Generic;
using NSubstitute;

namespace CsvViewer.Tests
{
    public abstract class TestsBase
    {
        protected static IDataSource CreateMockDataSource(IEnumerable<string> data)
        {
            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetData().Returns(data);

            return dataSource;
        }

        public struct SampleData
        {
            public static IEnumerable<string> DefaultDataFromKata
            {
                get
                {
                    return new List<string>
                {
                    "Name;Age;City",
                    "Peter;42;New York",
                    "Paul;57;London",
                    "Mary;35;Munich",
                    "Jaques;66;Paris",
                    "Yuri;23;Moscow",
                    "Stephanie;47;Stockholm",
                    "Nadia;29;Madrid"
                };
                }
            }
        }

        
    }
}