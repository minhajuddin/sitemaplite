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

        protected override void ParseData( StreamReader reader ) {
            var nodes = new List<SiteMapNode>();
            var id = 1;
            var currentParentId = 0;
            while ( !reader.EndOfStream ) {
                var line = reader.ReadLine();
                if ( IsParent( line ) ) {
                    currentParentId = id;
                }
                if ( line.Trim().Length == 0 ) continue;
                nodes.Add( ParseSiteMapNode( line, id++, currentParentId ) );
            }

            _nodes = nodes;
        }

        private bool IsParent( string line ) {
            var rx = new Regex( @"^\s+" );
            return !rx.IsMatch( line );
        }

        private SiteMapNode ParseSiteMapNode( string line, int id, int currentParentId ) {
            var tokens = line.Split( ':' );
            var title = tokens[0].Trim();
            var routeToken = tokens[1].Trim();
            tokens = routeToken.Split( '#' );
            var controller = tokens[0];
            var action = tokens[1];

            return new SiteMapNode { Id = id, Title = title, Action = action, Controller = controller, ParentId = currentParentId, Role = "Administrator" };
        }

        public override IEnumerable<SiteMapNode> GetAllNodes() {
            return _nodes;
        }
    }
}
