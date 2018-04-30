using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.UserService.DAL
{
    public class UserContext : SombraContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public UserContext(DbContextOptions<UserContext> options, bool seed) : base(options, seed)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}