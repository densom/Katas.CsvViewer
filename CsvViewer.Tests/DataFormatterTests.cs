using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace CsvViewer.Tests
{
    [TestFixture]
    public class DataFormatterTests
    {
        readonly List<string> _sampleData = new List<string>();
        private IDataSource _sampleDataSource;

        [TestFixtureSetUp]
        public void Setup()
        {
            _sampleData.Add("Name;Age;City");
            _sampleData.Add("Peter;42;New York");
            _sampleData.Add("Paul;57;London");
            _sampleData.Add("Mary;35;Munich");
            _sampleData.Add("Jaques;66;Paris");
            _sampleData.Add("Yuri;23;Moscow");
            _sampleData.Add("Stephanie;47;Stockholm");
            _sampleData.Add("Nadia;29;Madrid");

            _sampleDataSource = Substitute.For<IDataSource>();
            _sampleDataSource.GetData().Returns(_sampleData);
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