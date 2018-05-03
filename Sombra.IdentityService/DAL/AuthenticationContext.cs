using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class AuthenticationContext : SombraContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options, bool seed) : base(options, seed)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}