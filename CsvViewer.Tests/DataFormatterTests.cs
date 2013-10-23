using System.Collections.Generic;
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

    }
}