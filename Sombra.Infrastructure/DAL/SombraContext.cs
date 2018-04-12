using System.Reflection;
using Microsoft.EntityFrameworkCore;


namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext : DbContext
    {
        protected SombraContext(DbContextOptions options) : base(options){
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfigurations(GetType().Assembly);
        }
    }
}