using NUnit.Framework;
using TryParse;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var crawler = new CrawlPharmGroups();
            var groups = crawler.ObtainPharmGroups();
            Assert.Pass();
        }
    }
}