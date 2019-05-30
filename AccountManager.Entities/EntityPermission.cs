using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Entities
{
    public class EntityPermission
    {
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }    
    }
}
