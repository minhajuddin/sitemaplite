using NUnit.Framework;
using System.Web.Mvc;
using Moq;
using SiteMapLite.Html;

namespace SiteMapLite.Tests.UnitTests {
    [TestFixture]
    public class HtmlHelperExtensionTests {
        [Test]
        public void RenderMainNav_RendersTheRootNodes() {
            var mockContainer = new Mock<IViewDataContainer>();
            var helper = new HtmlHelper(new ViewContext(), mockContainer.Object);
            string result = helper.RenderMainNav(new { @class = "myNav", id = "menu" });
            Assert.AreEqual("<ul  class='myNav'  id='menu' ><li><a href='/Home/Index' title=HOME>HOME</a></li>\r\n<li><a href='/Dashboard/Index' title=Dashboard>Dashboard</a></li>\r\n<li><a href='/Administrator/Stats' title=My Stats>My Stats</a></li>\r\n</ul>", result);
        }
    }
}