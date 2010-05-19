using NUnit.Framework;
using System.Web.Mvc;
using Moq;
using SiteMapLite.Html;
using SiteMapLite.Core;

namespace SiteMapLite.Tests.UnitTests {
    [TestFixture]
    public class HtmlHelperExtensionTests {
        [SetUp]
        public void Setup() {
            CachedSiteMapService.Initialize( new SiteMapService( new JsonSiteMapReader() ) );
        }

        [Test, Ignore( "Unable to mock the route data" )]
        public void RenderMainNav_RendersTheRootNodes() {
            var mockContainer = new Mock<IViewDataContainer>();
            var helper = new HtmlHelper( new ViewContext(), mockContainer.Object );
            string result = helper.RenderMainNav( "Admin", new { @class = "myNav", id = "menu" } );
            Assert.AreEqual( "<ul  class='myNav'  id='menu' ><li><a href='/Home/Index' title=HOME>HOME</a></li>\r\n<li><a href='/Dashboard/Index' title=Dashboard>Dashboard</a></li>\r\n<li><a href='/Administrator/Stats' title=My Stats>My Stats</a></li>\r\n</ul>", result );
        }
    }
}