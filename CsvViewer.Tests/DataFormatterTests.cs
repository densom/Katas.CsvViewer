using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CsvViewer.Tests
{
    [TestFixture]
    public class DataFormatterTests : TestsBase
    {

        private IDataSource _sampleDataSource;

        [TestFixtureSetUp]
        public void Setup()
        {
            _sampleDataSource = CreateMockDataSource(SampleData.DefaultDataFromKata);
        }

        [Test]
        public void Constructor_DataIsSavedInDataProperty()
        {
            var data = new Data(_sampleDataSource);
            var dataFormatter = new DataFormatter(data);
            Assert.That(dataFormatter.Data, Is.EqualTo(data));
        }

        [Test]
        public void GetHeaderString_SupportsHeadersThatHaveTheLongestStrings()
        {
            var dataSource = CreateMockDataSource(new[] {"Name;Age;City"});
            var dataFormatter = new DataFormatter(new Data(dataSource));

            Assert.That(dataFormatter.GetHeaderString(), Is.EqualTo("Name|Age|City|"));
        }

        [Test]
        public void GetHeaderString_PadsHeadersForLongestRow_GivenOnePageOfData()
        {
            var dataSource = CreateMockDataSource(new[] {"Name;Age", "Dennis;37"});
            var dataFormatter = new DataFormatter(new Data(dataSource));

            Assert.That(dataFormatter.GetHeaderString(), Is.EqualTo("Name  |Age|"));
        }

        [Test]
        public void GetHeaderString_PadsForLongestRow_HandleLongestDataOnMultiplePages()
        {
            var dataSource = CreateMockDataSource(new[]
                {
                    "Name;Age", 
                    "Dennis;37",
                    "Dennis;37",
                    "Dennis;37",
                    "longest;00",
                });
            var dataFormatter = new DataFormatter(new Data(dataSource));

            Assert.That(dataFormatter.GetHeaderString(), Is.EqualTo("Name  |Age|"));
            Assert.That(dataFormatter.GetHeaderString(2), Is.EqualTo("Name   |Age|"));
        }

        [Test]
        public void GetHeaderSeperatorString_PadForLongestRow()
        {
            var dataSource = CreateMockDataSource(new[]
                {
                    "Name;Age", 
                });
            var dataFormatter = new DataFormatter(new Data(dataSource));

            Assert.That(dataFormatter.GetSeparatorString(), Is.EqualTo("----+---+"));
        }

        [Test]
        public void GetHeaderSeperatorString_PadForLongestRowOnMultiplePages()
        {
            var dataSource = CreateMockDataSource(new[]
                {
                    "Name;Age", 
                    "Dennis;37",
                    "Dennis;37",
                    "Dennis;37",
                    "longest;00",
                });
            var dataFormatter = new DataFormatter(new Data(dataSource));

            Assert.That(dataFormatter.GetSeparatorString(2), Is.EqualTo("-------+---+"));
        }

        [Test]
        public void GetRowString_PadForLongestRow()
        {
            var dataSource = CreateMockDataSource(new[]
                {
                    "Name;Age", 
                    "Dennis;37",
                    "Dennis;37",
                    "longest;00",
                });
            var dataFormatter = new DataFormatter(new Data(dataSource));

            Assert.That(dataFormatter.GetRowStrings().First(), Is.EqualTo("Dennis |37 |"));
        }

        [Test]
        public void GetRowString_PadForLongestRowOnMultiplePages()
        {
            var dataSource = CreateMockDataSource(new[]
                {
                    "Name;Age", 
                    "Dennis;37",
                    "Dennis;37",
                    "Dennis;37",
                    "longest;00",
                });
            var dataFormatter = new DataFormatter(new Data(dataSource));

            Assert.That(dataFormatter.GetRowStrings(2).First(), Is.EqualTo("longest|00 |"));
        }

        [Test]
        public void GetTable_SampleDataReturnsFirstPage()
        {
            var dataSource = CreateMockDataSource(new[]
                {
                    "Name;Age",
                    "Dennis;37",
                    "Meg;31",
                    "Bennett;0",
                    "Dan;34",
                    "Kelley;39",

                });
            var dataFormatter = new DataFormatter(new Data(dataSource));
            var expectedPage1 = 
                "Name   |Age|\r\n" +
                "-------+---+\r\n" +
                "Dennis |37 |\r\n" +
                "Meg    |31 |\r\n" +
                "Bennett|0  |\r\n";

            var expectedPage2 =
                "Name  |Age|\r\n" +
                "------+---+\r\n" +
                "Dan   |34 |\r\n" +
                "Kelley|39 |\r\n";

            Assert.That(dataFormatter.GetTable(1), Is.EqualTo(expectedPage1));
            Assert.That(dataFormatter.GetTable(2), Is.EqualTo(expectedPage2));
        }

    }
}