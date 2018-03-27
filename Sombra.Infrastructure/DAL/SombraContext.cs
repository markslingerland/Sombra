using System.Reflection;
using Microsoft.EntityFrameworkCore;


namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext : DbContext
    {
        public SombraContext(DbContextOptions options) : base(options){
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}