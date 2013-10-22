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
            _sampleData.Add("Mary;35;Munich");
            _sampleData.Add("Jaques;66;Paris");
            _sampleData.Add("Yuri;23;Moscow");
            _sampleData.Add("Stephanie;47;Stockholm");
            _sampleData.Add("Nadia;29;Madrid");

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
            Assert.That(csvViewerData.Header, Is.EquivalentTo(new[] { "Name", "Age", "City" }));
        }

        [Test]
        public void Rows_FirstRow_ContainsFirstDataRow()
        {
            var csvViewerData = new CsvViewerData(_sampleDataSource);

            Assert.That(csvViewerData.Rows.ElementAt(0), Is.EquivalentTo(new[] { "Peter", "42", "New York" }));
            Assert.That(csvViewerData.Rows.ElementAt(1), Is.EquivalentTo(new[] { "Paul", "57", "London" }));
        }

        [Test]
        public void GetPage_FirstPage_ReturnsRowsForDefaultPageSize()
        {
            var data = new CsvViewerData(_sampleDataSource);

            var pageData = data.GetPage(1);

            Assert.That(pageData.Count(), Is.EqualTo(3));
            Assert.That(pageData.ElementAt(0), Is.EqualTo(new[] { "Peter", "42", "New York" }));
            Assert.That(pageData.ElementAt(2), Is.EquivalentTo(new[] {"Mary","35","Munich"}));
        }
    }
}