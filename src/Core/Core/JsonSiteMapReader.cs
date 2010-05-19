using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SiteMapLite.Core
{
    public class JsonSiteMapReader : FileBasedSiteMapReader
    {
        protected IEnumerable<SiteMapNode> _nodes;

        public JsonSiteMapReader()
            : base("sitemap.json")
        {
        }

        public JsonSiteMapReader(Stream stream)
            : base(stream)
        {

        }
        public JsonSiteMapReader(string filename)
            : base(filename)
        {

        }

        protected override void ParseData(StreamReader reader)
        {
            string rawData = reader.ReadToEnd();
            if (string.IsNullOrEmpty(rawData))
            {
                throw new FileLoadException("File doesn't have any data in it");
            }
            _nodes = JsonConvert.DeserializeObject<IEnumerable<SiteMapNode>>(rawData) ?? new SiteMapNode[] { };
        }

        public override IEnumerable<SiteMapNode> GetAllNodes()
        {
            return _nodes;
        }
    }

    public abstract class FileBasedSiteMapReader : ISiteMapReader
    {

        public FileBasedSiteMapReader(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    string.Format("The sitemap json file {0} doesn't exist, Current Directory is {1}",
                                  filePath, Directory.GetCurrentDirectory()));
            }
            FileStream stream = File.Open(filePath, FileMode.Open);
            Init(stream);
        }

        public FileBasedSiteMapReader(Stream stream)
        {
            Init(stream);
        }

        protected virtual void Init(Stream stream)
        {
            using (stream)
            using (StreamReader sw = new StreamReader(stream))
            {
                ParseData(sw);
            }
        }

        protected abstract void ParseData(StreamReader reader);
        public abstract IEnumerable<SiteMapNode> GetAllNodes();
    }
}