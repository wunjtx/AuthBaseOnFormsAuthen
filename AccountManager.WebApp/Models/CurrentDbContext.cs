using AccountManager.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AccountManager.WebApp.Models
{
    public class CurrentDbContext
    {
        public static AccountDbContext GetDbContext()
        {
            AccountDbContext DbContext = HttpContext.Current.Items["DbContext"] as AccountDbContext;
            if (DbContext == null)
            {
                DbContext = new AccountDbContext();
                HttpContext.Current.Items["DbContext"] = DbContext;
            }
            return DbContext;
        }
    }
}