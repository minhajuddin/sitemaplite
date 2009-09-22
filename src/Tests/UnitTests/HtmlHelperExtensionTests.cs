using NUnit.Framework;
using System.Web.Mvc;
using Moq;
using SiteMapLite.Core;

namespace SiteMapLite.Tests.UnitTests {
    [TestFixture]
    public class HtmlHelperExtensionTests {
        [Test]
        public void RenderMainNav_RendersTheRootNodes() {
            var mockContainer = new Mock<IViewDataContainer>();
            var helper = new HtmlHelper(new ViewContext(), mockContainer.Object);
            string result = MenuHelper.RenderMainNav(helper);
            Assert.AreEqual("", result);
        }
    }
}