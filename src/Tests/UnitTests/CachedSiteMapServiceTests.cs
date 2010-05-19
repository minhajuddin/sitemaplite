using NUnit.Framework;
using SiteMapLite.Core;
using System;

namespace SiteMapLite.Tests.UnitTests {
    [TestFixture]
    public class CachedSiteMapServiceTests {

        [Test]
        public void CachedService_ReturnsTheSameService_WhenInvokedMoreThanOnce() {
            Configuration.Configure();
            ISiteMapService firstService = CachedSiteMapService.Service;
            ISiteMapService secondService = CachedSiteMapService.Service;
            Assert.That( firstService, Is.EqualTo( secondService ) );
        }

        [Test, Ignore]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void CachedService__ThrowsAnException_WhenNotConfigured() {
            ISiteMapService service = CachedSiteMapService.Service;
        }
    }
}