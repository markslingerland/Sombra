using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class AuthenticationContext : SombraContext<AuthenticationContext>
    {
        public AuthenticationContext() : base(GetOptions(), false) { }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
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