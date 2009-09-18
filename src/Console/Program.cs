using System.Collections.Generic;
using System.Linq;
using MvcSiteMap.Core;


namespace MvcSiteMap.Console {
    class Program {

        static void Main(string[] args) {
            ISiteMapReader reader = new JsonSiteMapReader();
            ISiteMapService service = new SiteMapService(reader);

            foreach (var role in "Administrator,Dashboard,Anonymous".Split(',')) {
                WL("------- Nodes for role {0} ----------", role);
                IEnumerable<SiteMapNode> jsonNodes = service.GetNodesForRole(role);
                PrintOrderedNodes(jsonNodes);
                WL("");
                WL("");
            }
        }

        private static void PrintOrderedNodes(IEnumerable<SiteMapNode> nodes) {
            WL("");
            WL("-------- Printing, Grouped nodes in a formatted fashion --------");
            WL("");
            foreach (var node in nodes) {
                WL("|{0}|", node.ToString());
                if (node.Children != null && node.Children.Count() > 1) {
                    foreach (var child in node.Children) {
                        WL("---|{0}|", child.Action.ToString());
                    }
                }
            }
        }


        private static void WL(string output, params object[] par) {
            System.Console.WriteLine(output, par);
        }

    }
}