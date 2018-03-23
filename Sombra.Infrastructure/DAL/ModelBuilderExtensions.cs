using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Sombra.Infrastructure.DAL
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
            EntityTypeConfiguration<TEntity> configuration)
            where TEntity : class, IEntity
        {
            modelBuilder.Entity<TEntity>(configuration.Configure);
        }

        public static void AddConfigurations(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var configurations = assembly.GetTypes().Where(t =>
                !t.IsAbstract && t.IsClass && typeof(EntityTypeConfiguration<>).IsAssignableFrom(t));

            var genericBuildMethod = typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.Entity));

            foreach (var config in configurations)
            {
                var entityType = config.GetGenericArguments()[0];
                var buildMethod = genericBuildMethod.MakeGenericMethod(entityType);
                var configConstructor = config.GetConstructors(BindingFlags.Public)[0];

                buildMethod.Invoke(modelBuilder, new []{ configConstructor.Invoke(new object[] { }) });
            }
        }
    }
}