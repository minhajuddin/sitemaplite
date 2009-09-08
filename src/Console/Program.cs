using System.Collections.Generic;
using System.Linq;
using JsonSiteMap;
using MvcSiteMap.Core;


namespace MvcSiteMap.Console {
    class Program {
        static void Main(string[] args) {
            IJsonSiteMapReader reader = new JsonSiteMapReader();
            IJsonSiteMapService service = new JsonSiteMapService(reader);

            foreach (var role in "Administrator,Dashboard,Anonymous".Split(',')) {
                WL("------- Nodes for role {0} ----------", role);
                IEnumerable<JsonNode> jsonNodes = service.GetNodesForRole(role);
                PrintOrderedNodes(jsonNodes);
                WL("");
                WL("");
            }



            return;

            //read the complete json file into a list of nodes
            //string jsonFile = ReadJsonFile("sitemap.json");
            ////get all the parent nodes, and child nodes arranged properly
            //IList<JsonNode> rawNodes = DeSerialize(jsonFile);
            //IEnumerable<JsonNode> groupedNodes = GroupNodes(rawNodes);

            //PrintOrderedNodes(groupedNodes);

        }

        //private static void DeSerializeTest() {
        //    IList<JsonNode> nodes = DeSerialize(
        //        @"[{'Id':1,'Action':'Index','Controller':'Home','Title':'HOME','ParentId':0},{'Id':1,'Action':'About','Controller':'Home','Title':'About Us','ParentId':1}]");
        //    foreach (var node in nodes) {
        //        WL(node.ToString());
        //    }
        //}

        //private static string ReadJsonFile(string jsonSiteMapPath) {
        //    using (StreamReader sw = File.OpenText(jsonSiteMapPath)) {
        //        return sw.ReadToEnd();
        //    }
        //}

        private static void PrintOrderedNodes(IEnumerable<JsonNode> nodes) {
            WL("");
            WL("-------- Printing, Grouped nodes in a formatted fashion --------");
            WL("");
            foreach (var node in nodes) {
                WL("|{0}|", node.ToString());
                if (node.Children != null && node.Children.Count() > 1) {
                    foreach (var child in node.Children) {
                        WL("---|{0}|", child.ToString());
                    }
                }
            }
        }

        //private static IEnumerable<JsonNode> GroupNodes(IList<JsonNode> nodes) {
        //    //get root nodes
        //    var rootNodes = nodes.Where(x => x.ParentId == 0).ToList();
        //    WL("Grouping {0} nodes in total and {1} root nodes", nodes.Count, rootNodes.Count());
        //    //populate child nodes for each root node
        //    foreach (var node in rootNodes) {
        //        var children = nodes.Where(x => x.ParentId == node.Id).ToList();
        //        WL("Assigning {0} children to {1} node", children == null ? -1 : children.Count(), node.Title);
        //        node.Children = children;
        //    }

        //    return rootNodes;
        //}

        //private static IList<JsonNode> DeSerialize(string rawJson) {
        //    IList<JsonNode> nodes = JsonConvert.DeserializeObject<IList<JsonNode>>(rawJson);
        //    return nodes;
        //}

        private static void WL(string output, params object[] par) {
            System.Console.WriteLine(output, par);
        }

        //private static void Serialize() {
        //    JsonNode node1 = new JsonNode { Id = 1, Action = "Index", Controller = "Home", Title = "HOME" };
        //    JsonNode node2 = new JsonNode { Id = 1, Action = "Index", Controller = "Home", Title = "HOME" };
        //    IList<JsonNode> nodes = new List<JsonNode> { node1, node2 };
        //    string output = JsonConvert.SerializeObject(nodes);
        //    WL(output);
        //}
    }
}