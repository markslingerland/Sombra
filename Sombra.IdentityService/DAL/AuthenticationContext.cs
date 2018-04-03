using Microsoft.EntityFrameworkCore;
using Sombra.IdentityService.DAL.Configurations;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class AuthenticationContext : SombraContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new CredentialEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new CredentialTypeEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new PermissionEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new RolePermissionEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.AddConfiguration(new UserRoleEntityTypeConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CredentialType> CredentialTypes { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
    }
}