using System.Collections.Generic;

namespace MvcSiteMap.Core {
    public interface ISiteMapService {
        IEnumerable<SiteMapNode> RawSiteMapNodes { get; }
        IEnumerable<SiteMapNode> GetNodesForRole(string role);
    }
}