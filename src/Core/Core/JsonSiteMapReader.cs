using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SiteMapLite.Core {
    public class JsonSiteMapReader : ISiteMapReader {
        protected IEnumerable<SiteMapNode> _nodes;

        public JsonSiteMapReader()
            : this("sitemap.json") {
        }

        public JsonSiteMapReader(string filePath) {
            if (!File.Exists(filePath)) {
                throw new FileNotFoundException(
                    string.Format("The sitemap json file {0} doesn't exist, Current Directory is {1}",
                                  filePath, Directory.GetCurrentDirectory()));
            }
            FileStream stream = File.Open(filePath, FileMode.Open);
            Init(stream);
        }

        public JsonSiteMapReader(Stream stream) {
            Init(stream);
        }

        protected virtual void Init(Stream stream) {
            using (stream)
            using (StreamReader sw = new StreamReader(stream)) {
                string rawData = sw.ReadToEnd();
                if (string.IsNullOrEmpty(rawData)) {
                    throw new FileLoadException("File doesn't have any data in it");
                }
                ParseData(rawData);
            }
        }

        protected virtual void ParseData(string rawData) {
            _nodes = JsonConvert.DeserializeObject<IEnumerable<SiteMapNode>>(rawData) ?? new SiteMapNode[] { };
        }

        public IEnumerable<SiteMapNode> GetAllNodes() {
            return _nodes;
        }
    }
}