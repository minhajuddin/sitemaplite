using System.Collections.Generic;

namespace MvcSiteMap.Core {
    public interface ISiteMapReader {
        IEnumerable<SiteMapNode> GetAllNodes();
    }
}