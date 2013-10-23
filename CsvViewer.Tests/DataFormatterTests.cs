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

            Assert.That(dataFormatter.GetRowStrings(2).First(), Is.EqualTo("Dennis |37 |"));
        }

    }
}