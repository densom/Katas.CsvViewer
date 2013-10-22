using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace CsvViewer.Tests
{
    [TestFixture]
    public class CsvViewerDataTests
    {
        readonly List<string> _sampleData = new List<string>();
        private IDataSource _sampleDataSource;

        [SetUp]
        public void Setup()
        {
            _sampleData.Add("Name;Age;City");
            _sampleData.Add("Peter;42;New York");
            _sampleData.Add("Paul;57;London");

            _sampleDataSource = Substitute.For<IDataSource>();
            _sampleDataSource.GetData().Returns(_sampleData);
        }


        [Test]
        public void Constructor_GivenIDataSource_SavesToDataSourceProperty()
        {
            var dataSource = Substitute.For<IDataSource>();
            var csvViewerData = new CsvViewerData(dataSource);

            Assert.That(csvViewerData.DataSource, Is.EqualTo(dataSource));
        }

        [Test]
        public void Header_ReturnsFirstRowInDataSource()
        {
            var csvViewerData = new CsvViewerData(_sampleDataSource);
            Assert.That(csvViewerData.Header, Is.EquivalentTo(new[] {"Name", "Age", "City"}));
        }

        [Test]
        public void Data_FirstRow_ContainsFirstDataRow()
        {
            var csvViewerData = new CsvViewerData(_sampleDataSource);

            Assert.That(csvViewerData.Rows.ElementAt(0), Is.EquivalentTo(new[] {"Peter", "42", "New York"}));
            Assert.That(csvViewerData.Rows.ElementAt(2), Is.EquivalentTo(new[] {"Paul", "57", "London"}));
        }
    }
}