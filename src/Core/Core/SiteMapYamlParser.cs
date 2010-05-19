using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SiteMapLite.Core {
    class SiteMapYamlParser {

        const string DEFAULT_ROLE = "Anonymous";
        string _previousRole;
        static Regex _childPatternRegex = new Regex( @"^\s+", RegexOptions.Compiled );

        private bool IsParent( string input ) {
            return !_childPatternRegex.IsMatch( input );
        }


        private SiteMapNode ParseSiteMapNode( string line, int id, int currentParentId ) {
            var tokens = line.Split( ':', '#', '>' );
            var title = tokens[0].Trim();
            var controller = tokens[1].Trim();
            var action = tokens[2].Trim();
            var role = tokens.Length == 4 ? tokens[3].Trim() : _previousRole ?? DEFAULT_ROLE;

            _previousRole = role;

            return new SiteMapNode { Id = id, Title = title, Action = action, Controller = controller, ParentId = currentParentId, Role = role };
        }

        public IEnumerable<SiteMapNode> Parse( StreamReader reader ) {
            var nodes = new List<SiteMapNode>();
            var id = 1;
            var currentParentId = 0;
            while ( !reader.EndOfStream ) {
                var line = reader.ReadLine();
                var isParent = IsParent( line );
                var parentId = isParent ? 0 : currentParentId;
                if ( isParent ) {
                    currentParentId = id;
                    _previousRole = DEFAULT_ROLE;
                }

                if ( line.Trim().Length != 0 ) {
                    nodes.Add( ParseSiteMapNode( line, id++, parentId ) );
                }
            }

            return nodes;
        }
    }
}
