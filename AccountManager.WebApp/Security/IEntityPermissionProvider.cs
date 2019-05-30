using AccountManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountManager.WebApp.Security
{
    public interface IEntityPermissionProvider
    {
        bool Authorzie<T>(T entity) where T : BaseEntity;
    }
}