using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccountManager.Entities;
using AccountManager.WebApp.Models;

namespace AccountManager.WebApp.Security
{
    public class EntityPermissionProvider : IEntityPermissionProvider
    {
        AccountDbContext db = CurrentDbContext.GetDbContext();
        public bool Authorzie<T>(T entity) where T : BaseEntity
        {
            //need cache
            var roleId = WorkContext.CurrentUser.Roles.Select(r => r.Id); 
            return db.EntityPermissions.Any(ep => ep.EntityType == typeof(T).Name && ep.EntityId==entity.Id && roleId.Contains(ep.RoleId));
        }
    }
}