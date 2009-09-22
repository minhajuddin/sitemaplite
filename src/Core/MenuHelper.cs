using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Mvc;
using System.Text;

namespace SiteMapLite.Core {
    public static class MenuHelper {
        public static string RenderMainNav(this HtmlHelper helper) {
            return RenderMainNav(helper, null);
        }

        public static string RenderMainNav(this HtmlHelper helper, object htmlAttributes) {
            ISiteMapReader reader = new JsonSiteMapReader();
            ISiteMapService service = new SiteMapService(reader);
            var nodes = service.GetNodesForRole("Administrator");
            StringBuilder sb = new StringBuilder();

            sb.Append("<ul ");

            AppendHtmlAttributes(sb, htmlAttributes);

            sb.Append(">");

            foreach (var node in nodes) {
                sb.AppendFormat("<li><a href='/{0}/{1}' title={2}>{2}</a></li>\r\n", node.Controller, node.Action, node.Title);
            }
            sb.Append("</ul>");
            return sb.ToString();
        }

        private static void AppendHtmlAttributes(StringBuilder sb, object htmlAttributes) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(htmlAttributes);
            foreach (PropertyDescriptor property in properties) {
                sb.AppendFormat(" {0}='{1}' ", property.Name.Trim('@')/* Remove the @ if the attribute is @class */, property.GetValue(htmlAttributes));
            }
        }
    }
}