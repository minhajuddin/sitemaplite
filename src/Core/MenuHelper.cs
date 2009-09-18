using System.Web.Mvc;
using System.Text;
namespace MvcSiteMap.Core {
    public static class MenuHelper {

        public static string RenderMainNav(this HtmlHelper helper) {
            ISiteMapReader reader = new JsonSiteMapReader();
            ISiteMapService service = new SiteMapService(reader);
            var nodes = service.GetNodesForRole("Administrator");
            StringBuilder sb = new StringBuilder();
            foreach (var node in nodes) {
                sb.AppendFormat("<a href='/{0}/{1}' title={2}>{2}</a>", node.Controller, node.Action, node.Title);
            }
            return sb.ToString();
        }

    }
}