using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;
namespace SiteMapLite.Core {
    public class YamlSiteMapReader : FileBasedSiteMapReader {
        protected IEnumerable<SiteMapNode> _nodes;

        public YamlSiteMapReader()
            : base( "sitemap.yaml" ) {
        }

        public YamlSiteMapReader( string filename )
            : base( filename ) {
        }

        public YamlSiteMapReader( Stream stream )
            : base( stream ) {
        }

        public override IEnumerable<SiteMapNode> GetAllNodes() {
            return _nodes;
        }

        protected override void ParseData( StreamReader reader ) {
            var parser = new SiteMapYamlParser();
            _nodes = parser.Parse( reader );
        }
    }
}
