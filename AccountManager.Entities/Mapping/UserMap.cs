using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Entities.Mapping
{
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(20);
            this.Property(t => t.Password).IsRequired().HasMaxLength(128);
            this.Property(t => t.Active);

            this.HasMany(t => t.Roles).WithMany().Map(m=> 
            {
                m.ToTable("UserRole");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });// withmany means role has many users
        }
    }
}
