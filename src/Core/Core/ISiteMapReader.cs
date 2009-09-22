using System.Collections.Generic;

namespace SiteMapLite.Core {
    public interface ISiteMapReader {
        IEnumerable<SiteMapNode> GetAllNodes();
    }
}