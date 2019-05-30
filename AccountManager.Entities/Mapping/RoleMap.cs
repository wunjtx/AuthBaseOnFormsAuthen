using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Entities.Mapping
{

    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(20);
            this.Property(t => t.Active);

            this.HasMany(t => t.Permissions).WithMany().Map(m =>
            {
                m.ToTable("RolePermission");
                m.MapLeftKey("RoleId");
                m.MapRightKey("Permission");
            });
        }
    }
    
}
