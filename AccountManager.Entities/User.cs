using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            this.Roles = new List<Role>();
        }
        public string Password { get; set; }  

        public virtual ICollection<Role> Roles { get; set; }
    }
}
