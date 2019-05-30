using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Entities
{
    public class Navigate:BaseEntity
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IconClassCode { get; set; }
        public int? ParentId { get; set; }
        public Navigate Parent { get; set; }
        public int? Sort { get; set; }
        public virtual ICollection<Navigate> Children { get; set; } = new List<Navigate>(); 

    }
}
