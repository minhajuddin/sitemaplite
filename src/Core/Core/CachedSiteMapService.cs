using System;

namespace SiteMapLite.Core {
    public class CachedSiteMapService {
        private static ISiteMapService _siteMapService;

        public static void Initialize(ISiteMapService siteMapService) {
            _siteMapService = siteMapService;
        }

        public static ISiteMapService Service {
            get {
                if (_siteMapService == null) {
                    throw new InvalidOperationException(
                    "Cached SiteMap Service hasn't been configured properly. Please configure it in Global.asax using Configuration.Configure() method");
                }
                return _siteMapService;
            }
        }
    }
}