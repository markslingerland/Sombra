using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
    public class AuthenticationContext : SombraContext<AuthenticationContext>
    {
        public AuthenticationContext() { }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Credential> Credentials { get; set; }
    }
}