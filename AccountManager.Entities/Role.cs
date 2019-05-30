using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            this.Permissions = new List<Permission>();
        }

        //in case large data for user do not use like this
        //public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
