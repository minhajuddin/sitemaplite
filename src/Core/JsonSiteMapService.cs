using System.Collections.Generic;
using System.IO;
using System.Linq;
using JsonSiteMap;
using Newtonsoft.Json;

namespace MvcSiteMap.Core {
    public interface IJsonSiteMapService {
        IEnumerable<JsonNode> JsonNodes { get; }
        IEnumerable<JsonNode> GetNodesForRole(string role);
    }

    public class JsonSiteMapService : IJsonSiteMapService {
        public IEnumerable<JsonNode> JsonNodes { get; private set; }

        public IEnumerable<JsonNode> GetNodesForRole(string role) {
            var filteredNodes = JsonNodes.Where(x => x.IsInRole(role)).ToList();
            return GroupNodes(filteredNodes);
        }

        public JsonSiteMapService(IJsonSiteMapReader jsonSiteMapReader) {
            string rawJsonData = jsonSiteMapReader.Read();
            Init(rawJsonData);
        }

        private void Init(string rawJsonData) {
            JsonNodes = DeSerialize(rawJsonData);
        }

        protected virtual IList<JsonNode> DeSerialize(string rawJson) {
            var nodes = JsonConvert.DeserializeObject<IList<JsonNode>>(rawJson);
            return nodes;
        }

        protected virtual IEnumerable<JsonNode> GroupNodes(IList<JsonNode> nodes) {
            //get root nodes
            var rootNodes = nodes.Where(x => x.ParentId == 0).ToList();
            //WL("Grouping {0} nodes in total and {1} root nodes", nodes.Count, rootNodes.Count());
            //populate child nodes for each root node
            foreach (var node in rootNodes) {
                var children = nodes.Where(x => x.ParentId == node.Id).ToList();
                //WL("Assigning {0} children to {1} node", children == null ? -1 : children.Count(), node.Title);
                node.Children = children;
            }
            return rootNodes;
        }

    }

    public interface IJsonSiteMapReader {
        string Read();
        string Read(string jsonSiteMapFilePath);
    }

    public class JsonSiteMapReader : IJsonSiteMapReader {

        public string Read() {
            return Read("sitemap.json");
        }

        public string Read(string jsonSiteMapPath) {
            if (!File.Exists(jsonSiteMapPath)) {
                throw new System.ArgumentException(string.Format("The sitemap json file {0} doesn't exist",
                                                                 jsonSiteMapPath));
            }
            using (StreamReader sw = File.OpenText(jsonSiteMapPath)) {
                return sw.ReadToEnd();
            }
        }
    }
}