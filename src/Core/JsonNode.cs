using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonSiteMap {
    public class JsonNode {
        public virtual int Id { get; set; }
        public virtual string Action { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Title { get; set; }
        public virtual string Role { get; set; }
        public int ParentId;

        public IEnumerable<JsonNode> Children;


        public override string ToString() {
            //return string.Format("Id = {0}, Action={1}, Controller={2}, Title={3}, ParentId={4}", Id, Action, Controller, Title, ParentId);
            return string.Format("{0}({1}/{2}) - [{3}]", Title, Controller, Action, Role);
        }

        public bool IsInRole(string role) {
            int count = Role.Split(',').Count(x => x.Trim().Equals(role, StringComparison.OrdinalIgnoreCase));
            return count != 0;
        }
    }
}