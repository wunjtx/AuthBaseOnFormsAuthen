using AccountManager.WebApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AccountManager.WebApp.Extens
{
    public static class HtmlHelperExtensions
    {
        static IAuthorizeProvider authorizeProvider = new AuthorizeProvider();
        public static MvcHtmlString ActionPermissionLink (this HtmlHelper htmlHelper, string linkText, string actionName)
        {
            string controllerName = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (authorizeProvider.Authorize(controllerName+actionName))
            {
                return htmlHelper.ActionLink(linkText, actionName);
            }
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString ActionPermissionLink(this HtmlHelper htmlHelper, string linkText, string actionName,string permissionName)
        {
            if (authorizeProvider.Authorize(permissionName))
            {
                return htmlHelper.ActionLink(linkText, actionName);
            }
            return MvcHtmlString.Empty;
        }
    }
}