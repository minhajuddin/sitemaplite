using System.IO;
using SiteMapLite.Core;

namespace SiteMapLite {
    public class Configuration {
        public static void Configure() {
            const string SITEMAP_FILE = "sitemap.json";
            Configure(SITEMAP_FILE);
        }

        public static void Configure(string filename) {
            string filePath = filename;

            if (!File.Exists(filePath)) {
                filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, filename);
            }
            if (!File.Exists(filePath)) {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            }

            ISiteMapReader siteMapReader = new JsonSiteMapReader(filePath);
            Configure(siteMapReader);
        }

        public static void Configure(ISiteMapReader siteMapReader) {
            ISiteMapService service = new SiteMapService(siteMapReader);
            CachedSiteMapService.Initialize(service);
        }
    }
}