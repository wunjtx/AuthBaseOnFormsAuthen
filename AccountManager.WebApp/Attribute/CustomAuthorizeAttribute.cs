using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Security;
using AccountManager.WebApp.Security;

namespace AccountManager.WebApp.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        private static readonly char[] _splitParameter = new[] { ',' };
        private string _permissionNames;
        private string[] _permissionNamesSplit = new string[0];
        public string PermissionNames
        {
            get { return _permissionNames ?? String.Empty; }
            set
            {
                _permissionNames = value;
                _permissionNamesSplit = SplitString(value);
            }
        }
        IAuthorizeProvider authorizeProvider = new AuthorizeProvider();
        //public CustomAuthorizeAttribute(params string[] permissionNames)
        //{
        //    this.PermissionNames = permissionNames ?? new string[0];
        //}
        public CustomAuthorizeAttribute():base()
        {

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (skipAuthorization || filterContext.Result is HttpUnauthorizedResult)
            {
                return;
            }
            if (filterContext.Result==null)
            {
                string actionName = filterContext.ActionDescriptor.ActionName.ToUpper();
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper();
                List<string> actionPermissionNames = _permissionNamesSplit.ToList();
                if (!_permissionNamesSplit.Any(p => p == controllerName + actionName))
                {
                    actionPermissionNames.Add(controllerName + actionName);
                }

                if (actionPermissionNames.Any(pName => authorizeProvider.Authorize(pName)))
                {
                    return;
                }
            }
            filterContext.Result = new HttpUnauthorizedResult();
            //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        internal static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(_splitParameter)
                        let trimmed = piece.Trim().ToUpper()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
    }
}