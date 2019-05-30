using AccountManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.WebApp.Security
{
    public interface IAuthenticationProvider
    {
        void SignIn(User user, bool rememberMe);
        void SignOut();
        User GetAuthorizedUser();
    }
}
