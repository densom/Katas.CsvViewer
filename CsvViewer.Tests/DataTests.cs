using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace CsvViewer.Tests
{
    [TestFixture]
    public class DataTests : TestsBase
    {
        private IDataSource _sampleDataSource;

        [TestFixtureSetUp]
        public void Setup()
        {
            _sampleDataSource = CreateMockDataSource(SampleData.DefaultDataFromKata);
        }


        [Test]
        public void Constructor_GivenIDataSource_SavesToDataSourceProperty()
        {
            var dataSource = Substitute.For<IDataSource>();
            var data = new Data(dataSource);

            Assert.That(data.DataSource, Is.EqualTo(dataSource));
        }

        [Test]
        public void Header_ReturnsFirstRowInDataSource()
        {
            var data = new Data(_sampleDataSource);
            Assert.That(data.Header, Is.EquivalentTo(new[] { "Name", "Age", "City" }));
        }

        [Test]
        public void Rows_FirstRow_ContainsFirstDataRow()
        {
            var data = new Data(_sampleDataSource);

            Assert.That(data.Rows.ElementAt(0), Is.EquivalentTo(new[] { "Peter", "42", "New York" }));
            Assert.That(data.Rows.ElementAt(1), Is.EquivalentTo(new[] { "Paul", "57", "London" }));
        }

        [Test]
        public void GetPage_FirstPage_ReturnsRowsForDefaultPageSize()
        {
            var data = new Data(_sampleDataSource);

            var pageData = data.GetPage(1);

            Assert.That(pageData.Count(), Is.EqualTo(3));
            Assert.That(pageData.ElementAt(0), Is.EquivalentTo(new[] { "Peter", "42", "New York" }));
            Assert.That(pageData.ElementAt(2), Is.EquivalentTo(new[] { "Mary", "35", "Munich" }));
        }

        [Test]
        public void GetPage_SecondPage_ReturnsRowsForDefaultPageSize()
        {
            var data = new Data(_sampleDataSource);

            var pageData = data.GetPage(2);

            Assert.That(pageData.Count(), Is.EqualTo(3));
            Assert.That(pageData.ElementAt(0), Is.EquivalentTo(new[] { "Jaques", "66", "Paris" }));
            Assert.That(pageData.ElementAt(2), Is.EquivalentTo(new[] { "Stephanie", "47", "Stockholm" }));
        }

        [Test]
        public void GetPage_ThirdPage_ReturnsLessThanPageSizeWhenOnlyOneRowLeft()
        {
            var data = new Data(_sampleDataSource);

            var pageData = data.GetPage(3);
            var count = pageData.Count();
            Assert.That(count, Is.EqualTo(1));
            Assert.That(pageData.ElementAt(0), Is.EquivalentTo(new[] { "Nadia", "29", "Madrid" }));
        }

        [Test]
        public void GetPageWithHeader()
        {
            var data = new Data(_sampleDataSource);

            Assert.That(data.GetPageWithHeaders(1).ElementAt(0), Is.EquivalentTo(new[] {"Name", "Age","City"}));
            Assert.That(data.GetPageWithHeaders(1).ElementAt(1), Is.EquivalentTo(new[] {"Peter", "42","New York"}));
        }

        [Test]
        public void PageCount_For7DataRows_Returns3()
        {
            var data = new Data(_sampleDataSource);

            Assert.That(data.PageCount, Is.EqualTo(3));
        }
    }
}