using AccountManager.Entities.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Text;

namespace AccountManager.Entities
{
    public class AccountDbContext:DbContext
    {
        public AccountDbContext():base("AccountManager")
        {

        }
        static AccountDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AccountDbContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EntityPermission> EntityPermissions { get; set; }
        public DbSet<Navigate> Navigates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new EntityPermissionMap());
            modelBuilder.Configurations.Add(new NavigateMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
