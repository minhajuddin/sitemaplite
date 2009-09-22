using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;
using System.Text;
using SiteMapLite.Core;

namespace SiteMapLite.Tests.UnitTests {
    [TestFixture]
    public class JsonSiteMapReaderTests {

        [Test]
        public void GetAllNodes_ReturnsAnIEnumerableOfSiteMapNodesWithMoreThanOneNode() {
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(GetValidRawJson()));
            ISiteMapReader reader = new JsonSiteMapReader(stream);
            var nodes = reader.GetAllNodes();
            Assert.IsNotNull(nodes);
            Assert.IsInstanceOf<IEnumerable<SiteMapNode>>(nodes);
            Assert.AreEqual(1, nodes.Count());
        }

        [Test]
        public void GetAllNodes_ParsesTheNodeFileAsExpected() {
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(GetValidRawJson()));
            ISiteMapReader reader = new JsonSiteMapReader(stream);
            var nodes = reader.GetAllNodes();
            SiteMapNode node = nodes.FirstOrDefault();

            Assert.AreEqual(1, node.Id);
            Assert.AreEqual("Index", node.Action);
            Assert.AreEqual("Home", node.Controller);
            Assert.AreEqual("HOME", node.Title);
            Assert.AreEqual(0, node.ParentId);
            Assert.AreEqual("Anonymous,Administrator,Dashboard", node.Role);
        }


        [Test]
        public void GetAllNodes_ReturnsAnEmptyList_WhenFileDoesntHaveAnyNodes() {
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(" "));
            ISiteMapReader reader = new JsonSiteMapReader(stream);
            var nodes = reader.GetAllNodes();
            Assert.IsNotNull(nodes);
            Assert.IsInstanceOf<IEnumerable<SiteMapNode>>(nodes);
            Assert.AreEqual(0, nodes.Count());
        }

        [Test]
        [ExpectedException(typeof(FileLoadException))]
        public void GetAllNodes_ThrowsAFileLoadException_IfFileIsEmpty() {
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(""));
            ISiteMapReader reader = new JsonSiteMapReader(stream);
            var nodes = reader.GetAllNodes();
            Assert.IsNotNull(nodes);
            Assert.IsInstanceOf<IEnumerable<SiteMapNode>>(nodes);
        }


        //helpers
        private string GetValidRawJson() {
            return
                @"[ {'Id':1,'Action':'Index','Controller':'Home','Title':'HOME','ParentId':0,'Role':'Anonymous,Administrator,Dashboard'}]";
        }
    }
}