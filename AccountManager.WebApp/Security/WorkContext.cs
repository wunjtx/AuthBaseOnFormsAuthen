using AccountManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountManager.WebApp.Security
{
    public class WorkContext
    {
        public static User CurrentUser
        {
            get
            {
                return new AuthenticationProvider().GetAuthorizedUser();
            }
        }
    }
}