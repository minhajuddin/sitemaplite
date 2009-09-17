using NUnit.Framework;
using MvcSiteMap.Core;

namespace Tests.UnitTests {
    [TestFixture]
    public class SiteMapNodeTests {

        [Test]
        public void IsInRole_ReturnsTrue_IfTheSiteMapNodeHasTheInputRoleOnIt() {
            var node = new SiteMapNode { Role = "Administrator,Customer" };
            bool result = node.IsInRole("Customer");
            Assert.IsTrue(result);
        }

        [Test]
        public void IsInRole_ReturnsFalse_IfTheSiteMapNodeDoesNotHaveTheInputRoleOnIt() {
            var node = new SiteMapNode { Role = "Administrator,Customer" };
            bool result = node.IsInRole("User");
            Assert.IsFalse(result);
        }

        [Test]
        public void IsInRole_ReturnsFalse_WhenInputRoleIsNull() {
            var node = new SiteMapNode { Role = "Administrator,Customer" };
            bool result = node.IsInRole(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsInRole_ReturnsTrue_IfTheSiteMapNodeDoesNotHaveAnyRoleOnIt([Values(" ", null, "")]string nodeRole) {
            var node = new SiteMapNode { Role = nodeRole };
            bool result = node.IsInRole("User");
            Assert.IsTrue(result);
        }

        [Test]
        public void IsRootNode_ReturnsTrue_IfTheParentIdIs0() {
            var node = new SiteMapNode { Id = 1, ParentId = 0 };
            bool result = node.IsRootNode;
            Assert.IsTrue(result);
        }


        [Test]
        public void IsRootNode_ReturnsFalse_IfTheParentIdIsNot0() {
            var node = new SiteMapNode { Id = 1, ParentId = 4 };
            bool result = node.IsRootNode;
            Assert.IsFalse(result);
        }
    }
}