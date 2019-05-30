using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Entities.Mapping
{
    public class EntityPermissionMap:EntityTypeConfiguration<EntityPermission>
    {
        public EntityPermissionMap()
        {
            this.HasKey(t => new { t.EntityId,t.EntityType,t.RoleId });
            this.Property(t => t.EntityType).HasMaxLength(20);

            this.HasRequired(t => t.Role).WithMany().HasForeignKey(f=>f.RoleId).WillCascadeOnDelete(true);//when delete roleid, will automaticlly delete this record in database
        }
    }
}
