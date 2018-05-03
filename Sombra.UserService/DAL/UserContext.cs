using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.UserService.DAL
{
    public class UserContext : SombraContext<UserContext>
    {
        public UserContext() { }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}