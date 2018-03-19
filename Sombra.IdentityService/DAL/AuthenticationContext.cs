using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sombra.IdentityService.DAL
{
    static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(
            this ModelBuilder modelBuilder, 
            DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
        {     
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }
    }

    abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {     
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }

    public class AuthenticationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CredentialType> CredentialTypes { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("AUTHENTICATION_DB_CONNECTIONSTRING"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddConfiguration(new CredentialEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new CredentialTypeEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new PermissionEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new RolePermissionEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new UserRolEntityTypeConfiguration());                  
        }
    }
}