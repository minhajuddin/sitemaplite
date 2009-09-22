using System.Collections.Generic;

namespace SiteMapLite.Core {
    public interface ISiteMapService {
        IEnumerable<SiteMapNode> RawSiteMapNodes { get; }
        IEnumerable<SiteMapNode> GetNodesForRole(string role);
    }
}