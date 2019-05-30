using AccountManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.WebApp.Security
{
    public interface IAuthorizeProvider
    {
        bool Authorize(string permissionName, User user);
        bool Authorize(string permissionName);
    }
}
