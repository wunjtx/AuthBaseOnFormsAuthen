using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccountManager.Entities;
using AccountManager.WebApp.Models;

namespace AccountManager.WebApp.Security
{
    public class AuthorizeProvider : IAuthorizeProvider
    {
        AccountDbContext db = CurrentDbContext.GetDbContext();
        bool? result;
        public bool Authorize(string permissionName, User user)
        {
            try {
                result = HttpContext.Current.Session["IsAuthorized"] as bool?;//session can be replaced by redis
                if (result!=null)
                {
                    return result==true;
                }
                var tempResult = user.Roles.Where(r => r.Active).Any(role => Authorize(permissionName, role));
                HttpContext.Current.Session.Add("IsAuthorized", tempResult);//session can be replaced by redis
                return tempResult;
            }
            catch
            {
                return false;
            }
        }
        public bool Authorize(string permissionName)
        {
            try { 
                return Authorize(permissionName, WorkContext.CurrentUser);
            }
            catch
            {
                return false;
            }
        }
        protected virtual bool Authorize(string permissionName,Role role)
        {
            try {
                result = HttpContext.Current.Session["IsAuthorized"] as bool?;//session can be replaced by redis
                if (result != null)
                {
                    return result == true;
                }
                var tempResult = role.Permissions.Any(p => p.Name.Equals(permissionName, StringComparison.InvariantCultureIgnoreCase));
                HttpContext.Current.Session.Add("IsAuthorized", tempResult);//session can be replaced by redis
                return tempResult;
            }catch
            {
                return false;
            }
        }
    }
}