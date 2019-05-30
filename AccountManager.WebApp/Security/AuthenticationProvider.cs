using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccountManager.Entities;
using System.Web.Security;
using AccountManager.WebApp.Models;

namespace AccountManager.WebApp.Security
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        AccountDbContext dbContext = CurrentDbContext.GetDbContext();
        User user=default(User);
        public User GetAuthorizedUser()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext!=null && httpContext.Request!=null && httpContext.Request.IsAuthenticated && (httpContext.User.Identity is FormsIdentity))
            {
                user = httpContext.Session["LoginUser"] as User;//can replace session to redis
                if (user != null)
                {
                    return user;
                }

                FormsIdentity formIdentity = httpContext.User.Identity as FormsIdentity;
                if (formIdentity==null)
                {
                    return null;
                }

                string userName = formIdentity.Ticket.Name;
                string userData = formIdentity.Ticket.UserData;
                if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(userData))
                {
                    return null;
                }
                user =  dbContext.Users.SingleOrDefault(u => u.Name == userName);
                httpContext.Session.Add("LoginUser", user);//can replace session to redis
                httpContext.Session.Add("LoginIdentity", formIdentity);//can replace session to redis
                return user;
            }
            return null;
        }

        public void SignIn(User user, bool rememberMe)
        {
            string userData = Guid.NewGuid().ToString();
            var ticket = new FormsAuthenticationTicket(version: 1, name: user.Name, issueDate: DateTime.Now, expiration: DateTime.Now.AddDays(7), isPersistent: rememberMe, userData: userData);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { HttpOnly = true };
            HttpContext.Current.Response.Cookies.Add(cookie);
            //FormsAuthentication.SetAuthCookie(user.Name, rememberMe, FormsAuthentication.FormsCookiePath);
            HttpContext.Current.Session.Add("LoginUser", user);//can replace session to redis
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            try
            {
                HttpContext.Current.Session.Remove("IsAuthorized");
                HttpContext.Current.Session.Remove("LoginUser");
                HttpContext.Current.Session.Remove("LoginIdentity");
            }
            catch
            {
                return;
            }
        }
    }
}