using System.Linq;
using NUnit.Framework;

namespace CsvViewer.Tests
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void AddHeader_SetsHeaderProperty_ToArrayOfMatchingStrings()
        {
            var data = new Data();
            data.SetHeader("h1", "h2");

            Assert.That(data.Header, Is.EqualTo(new[] {"h1", "h2"}));
        }

        [Test]
        public void AddRow_SetsFirstRow_EqualToValuesPassedIn()
        {
            var data = new Data();
            data.AddRow("r1-c1", "r1-c2");
            Assert.That(data.Rows.First(), Is.EqualTo(new [] {"r1-c1", "r1-c2"}));
        }
    }
}