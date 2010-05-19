using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SiteMapLite.Core;

namespace SiteMapLite.Tests.IntegrationTests {
    [TestFixture]
    public class YamlSiteMapReaderTests {

        [Test]
        public void CanReadNodesFromTheDefaultFileIfItIsPresent() {
            var reader = new YamlSiteMapReader();
            var nodes = reader.GetAllNodes();
            Assert.IsNotNull( nodes );
            Assert.AreEqual( 6, nodes.Count() );
        }
        [Test]
        public void CanReadNodesFromACustomYamlFile() {
            var reader = new YamlSiteMapReader( "sitemap.yaml" );
            var nodes = reader.GetAllNodes();

            var home = nodes.First();
            Assert.AreEqual( "Home", home.Title );
            Assert.AreEqual( "home", home.Controller );
            Assert.AreEqual( "index", home.Action );
            Assert.AreEqual( 1, home.Id );

            var second = nodes.Single( x => x.Title == "Faqs" );
            Assert.AreEqual( 2, second.Id );
        }

        [Test]
        public void ParentInfoIsPopulatedProperly() {
            var reader = new YamlSiteMapReader();
            var nodes = reader.GetAllNodes();

            var node = nodes.Single( x => x.Title == "Faqs" );
            Assert.AreEqual( 1, node.ParentId );

            node = nodes.Single( x => x.Title == "Add" );
            Assert.AreEqual( 4, node.ParentId );
        }
    }
}
