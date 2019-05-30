using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Entities.Mapping
{
    public class NavigateMap : EntityTypeConfiguration<Navigate>
    {
        public NavigateMap()
        {
            this.HasKey(n => n.Id);
            this.Property(n => n.ActionName).HasMaxLength(50).IsOptional();
            this.Property(n => n.ControllerName).HasMaxLength(50).IsOptional();
            this.Property(n => n.IconClassCode).HasMaxLength(50).IsOptional();
            this.HasOptional(n => n.Parent);
            this.Property(n => n.Sort);
            this.Property(n => n.Name).IsRequired().HasMaxLength(20);
            this.Property(n => n.Active);

        }
    }
}
