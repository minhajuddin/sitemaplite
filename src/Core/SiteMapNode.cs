using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSiteMap.Core {
    public class SiteMapNode {

        public virtual int Id { get; set; }
        public virtual string Action { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Title { get; set; }
        public virtual string Role { get; set; }
        public int ParentId { get; set; }

        public IEnumerable<SiteMapNode> Children;

        public virtual bool IsInRole(string role) {
            if (Role == null || Role.Trim().Length == 0) {
                return true;
            }

            int count = Role.Split(',').Count(x => x.Trim().Equals(role, StringComparison.OrdinalIgnoreCase));
            return count != 0;
        }

        public virtual bool IsRootNode {
            get { return ParentId == 0; }
        }
    }
}