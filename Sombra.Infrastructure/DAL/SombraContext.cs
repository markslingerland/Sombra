using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddConfigurations(Assembly.GetExecutingAssembly());
        }
    }
}