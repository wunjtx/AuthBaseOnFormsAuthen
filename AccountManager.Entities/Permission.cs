using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Entities
{
    public class Permission:BaseEntity
    {
        public string Category { get; set; }

        public string Description { get; set; }

        //public IEnumerable<Permission> Implies { get; set; }//fro example: delete permission must have get access permission

    }
}
