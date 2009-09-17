using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using MvcSiteMap.Core;

namespace Tests.UnitTests {
    [TestFixture]
    public class SiteMapServiceTests {

        [Test]
        public void RawSiteMapNodes_ReturnsTheRawNodesUnchanged() {
            var mockReader = new Mock<ISiteMapReader>();
            mockReader.Setup(x => x.GetAllNodes()).Returns(new[] { new SiteMapNode { Id = 1 } });
            ISiteMapService service = new SiteMapService(mockReader.Object);

            IEnumerable<SiteMapNode> nodes = service.RawSiteMapNodes;

            Assert.IsNotNull(nodes);
            Assert.AreEqual(1, nodes.Count());
        }

        [Test]
        public void GetNodesForRoles_ReturnsAllRolesIfAllOfThemAreRootNodes() {
            var mockReader = GetMockReader();
            ISiteMapService service = new SiteMapService(mockReader.Object);

            var nodes = service.GetNodesForRole("Admin");

            Assert.IsNotNull(nodes);
            Assert.AreEqual(2, nodes.Count());
        }

        [Test]
        public void GetNodesForRoles_GroupsTheNodesProperly() {
            Mock<ISiteMapReader> mockReader = GetMockReader();
            ISiteMapService service = new SiteMapService(mockReader.Object);

            var nodes = service.GetNodesForRole("Admin");

            Assert.IsNotNull(nodes);

            SiteMapNode firstNode = nodes.SingleOrDefault(x => x.Id == 1);
            SiteMapNode secondNode = nodes.SingleOrDefault(x => x.Id == 4);

            Assert.AreEqual(2, firstNode.Children.Count());
            Assert.AreEqual(3, secondNode.Children.Count());
        }

        private Mock<ISiteMapReader> GetMockReader() {
            var mockReader = new Mock<ISiteMapReader>();
            mockReader.Setup(x => x.GetAllNodes()).Returns(new[] { 
                                                                     new SiteMapNode { Id = 1, ParentId = 0 }, 
                                                                     new SiteMapNode { Id = 2, ParentId = 1 }, 
                                                                     new SiteMapNode { Id = 3, ParentId = 1 }, 
                                                                     new SiteMapNode { Id = 4, ParentId = 0 }, 
                                                                     new SiteMapNode { Id = 5, ParentId = 4 }, 
                                                                     new SiteMapNode { Id = 6, ParentId = 4 }, 
                                                                     new SiteMapNode { Id = 7, ParentId = 4 } 
                                                                 });
            return mockReader;
        }
    }
}