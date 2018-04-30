using Microsoft.EntityFrameworkCore;

namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext : DbContext
    {
        protected SombraContext(DbContextOptions options) : base(options){
            if (Database.IsSqlServer())
                Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfigurations(GetType().Assembly);
            Seed(modelBuilder);
        }

        protected void Seed(ModelBuilder modelBuilder)
        {
        }
    }
}