using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccountManager.Entities;
using AccountManager.WebApp.Controllers;

namespace AccountManager.WebApp.Security
{
    public class PermissionProvider : IPermissionProvicer
    {
        public IEnumerable<Permission> GetPermissions()
        {
            //"ControllerName"+"ActionName"
            List<Permission> permissions = new List<Permission>();
            permissions.Add(new Permission() { Name = "User"+nameof(UserController.Create), Category = "UserInfo", Description = "Create",Active=true });
            permissions.Add(new Permission() { Name = "User" + nameof(UserController.Delete), Category = "UserInfo", Description = "Delete", Active = true });
            permissions.Add(new Permission() { Name = "User" + nameof(UserController.Details), Category = "UserInfo", Description = "Details", Active = true });
            permissions.Add(new Permission() { Name = "User" + nameof(UserController.Edit), Category = "UserInfo", Description = "Edit", Active = true });
            permissions.Add(new Permission() { Name = "User" + nameof(UserController.Search), Category = "UserInfo", Description = "Search", Active = true });
            permissions.Add(new Permission() { Name = "Role"+nameof(RoleController.Create), Category = "RoleInfo", Description = "Create", Active = true });
            permissions.Add(new Permission() { Name = "Role" + nameof(RoleController.Details), Category = "RoleInfo", Description = "Details", Active = true });
            permissions.Add(new Permission() { Name = "Role" + nameof(RoleController.Delete), Category = "RoleInfo", Description = "Delete", Active = true });
            permissions.Add(new Permission() { Name = "Role" + nameof(RoleController.Edit), Category = "RoleInfo", Description = "Edit", Active = true });
            permissions.Add(new Permission() { Name = "Role" + nameof(RoleController.Authorize), Category = "RoleInfo", Description = "Authorize", Active = true });
            permissions.Add(new Permission() { Name = "Test"+nameof(TestController.Index), Category = "Test", Description = "Index", Active = true });
            permissions.Add(new Permission() { Name = "Test" + nameof(TestController.GetCode1), Category = "Test", Description = "Code1", Active = true });
            permissions.Add(new Permission() { Name = "Test" + nameof(TestController.GetCode2), Category = "Test", Description = "Code2", Active = true });

            return permissions;
        }
    }
}