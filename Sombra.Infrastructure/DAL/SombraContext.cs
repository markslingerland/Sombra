using Microsoft.EntityFrameworkCore;

namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext : DbContext
    {
        private readonly bool _seed = true;

        protected SombraContext(DbContextOptions options) : base(options)
        {
            if (Database.IsSqlServer()) Database.Migrate();
        }

        protected SombraContext(DbContextOptions options, bool seed) : base(options)
        {
            _seed = seed;
            if (Database.IsSqlServer()) Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfigurations(GetType().Assembly);

            if (_seed) Seed(modelBuilder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
        }
    }
}