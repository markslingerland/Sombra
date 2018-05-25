using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.UserService.DAL
{
    public class UserContext : SombraContext<UserContext>
    {
        public UserContext() { }

        public UserContext(DbContextOptions<UserContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }

        public DbSet<User> Users { get; set; }
    }
}