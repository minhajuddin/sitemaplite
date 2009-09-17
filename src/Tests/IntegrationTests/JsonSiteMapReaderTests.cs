using System.Linq;
using MvcSiteMap.Core;
using NUnit.Framework;
using System.IO;

namespace Tests.IntegrationTests {
    [TestFixture]
    public class JsonSiteMapReaderTests {

        [Test]
        public void SiteMapReader_CanReadNodesFromTheDefaultFileIfItIsPresent() {
            var reader = new JsonSiteMapReader();
            var nodes = reader.GetAllNodes();
            Assert.IsNotNull(nodes);
            Assert.AreEqual(8, nodes.Count());
        }

        [Test]
        public void SiteMapReader_CanReadNodesFromACustomJsonFile() {
            var reader = new JsonSiteMapReader("sitemap.json");
            var nodes = reader.GetAllNodes();
            Assert.IsNotNull(nodes);
            Assert.AreEqual(8, nodes.Count());
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SiteMapReader_ThrowsAnExceptionIfTheInputFileDoesNotExist() {
            var reader = new JsonSiteMapReader("nonexistant.json");
            var nodes = reader.GetAllNodes();
        }


    }
}