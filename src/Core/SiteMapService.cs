using System.Collections.Generic;
using System.Linq;

namespace MvcSiteMap.Core {
    public class SiteMapService : ISiteMapService {

        public virtual IEnumerable<SiteMapNode> RawSiteMapNodes { get; private set; }

        public virtual IEnumerable<SiteMapNode> GetNodesForRole(string role) {
            var filteredNodes = RawSiteMapNodes.Where(x => x.IsInRole(role)).ToList();
            return GroupNodes(filteredNodes);
        }

        public SiteMapService(ISiteMapReader siteMapReader) {
            RawSiteMapNodes = siteMapReader.GetAllNodes();
        }

        protected virtual IEnumerable<SiteMapNode> GroupNodes(IList<SiteMapNode> nodes) {
            //get root nodes
            var rootNodes = nodes.Where(x => x.IsRootNode).ToList();
            //populate child nodes for each root node
            foreach (var node in rootNodes) {
                var children = nodes.Where(x => x.ParentId == node.Id).ToList();
                node.Children = children;
            }
            return rootNodes;
        }

    }
}