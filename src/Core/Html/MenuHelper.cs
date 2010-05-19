using System.ComponentModel;
using System.Web.Mvc;
using System.Text;
using SiteMapLite.Core;
using System;

namespace SiteMapLite.Html {
    public static class MenuHelper {


        public static string RenderMainNav( this HtmlHelper helper, string role ) {
            return RenderMainNav( helper, role, null );
        }

        public static string RenderMainNav( this HtmlHelper helper, string role, object htmlAttributes ) {
            return helper.RenderMainNav( role, htmlAttributes, "selected" );
        }

        public static string RenderMainNav( this HtmlHelper helper, string role, object htmlAttributes, string selectedCssClass ) {

            var nodes = CachedSiteMapService.Service.GetNodesForRole( role );
            StringBuilder sb = new StringBuilder();

            sb.Append( "<ul " );

            AppendHtmlAttributes( sb, htmlAttributes );

            sb.Append( ">" );
            string cssAttributeChunk = string.Format( "class='{0}'", selectedCssClass );

            foreach ( var node in nodes ) {
                sb.AppendFormat( "<li {3}><a href='/{0}/{1}' title='{2}'>{2}</a></li>\r\n", node.Controller, node.Action, node.Title, helper.IsUnderCurrentParentNode( node.Controller ) ? cssAttributeChunk : "" );
            }
            sb.Append( "</ul>" );
            return sb.ToString();
        }

        private static void AppendHtmlAttributes( StringBuilder sb, object htmlAttributes ) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties( htmlAttributes );
            foreach ( PropertyDescriptor property in properties ) {
                sb.AppendFormat( " {0}='{1}' ", property.Name, property.GetValue( htmlAttributes ) );
            }
        }

        public static bool IsUnderCurrentParentNode( this HtmlHelper helper, string controller ) {
            string currentController = helper.GetCurrentController();
            return currentController.Equals( controller, StringComparison.OrdinalIgnoreCase );
        }

        public static bool IsSameAsCurrentRoute( this HtmlHelper helper, string controller, string action ) {
            string currentAction = helper.GetCurrentAction();
            string currentController = helper.GetCurrentController();
            return currentAction.Equals( action, StringComparison.OrdinalIgnoreCase ) && currentController.Equals( controller, StringComparison.OrdinalIgnoreCase );
        }

        public static string GetCurrentAction( this HtmlHelper helper ) {
            return helper.ViewContext.RouteData.GetRequiredString( "action" );
        }

        public static string GetCurrentController( this HtmlHelper helper ) {
            return helper.ViewContext.RouteData.GetRequiredString( "controller" );
        }
    }
}