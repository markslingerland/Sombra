using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sombra.Infrastructure.DAL
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
            EntityTypeConfiguration<TEntity> configuration)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>(configuration.Configure);
        }

        public static void AddConfigurations(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var configurations = assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && t.BaseType != null
                                                                && t.BaseType.IsGenericType
                                                                && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();

            var genericBuildMethod = typeof(ModelBuilder).GetMethods()
                .First(m => m.IsGenericMethod && m.Name == nameof(ModelBuilder.Entity) && m.GetParameters().Length == 1);

            foreach (var config in configurations)
            {
                var entityType = config.BaseType.GetGenericArguments()[0];
                var buildMethod = genericBuildMethod.MakeGenericMethod(entityType);

                var configInstance = Activator.CreateInstance(config);
                var configureMethod = config.GetMethod("Configure"); // nameof() does not work with generic types (EntityTypeConfiguration<T> in this case)
                var actionType = typeof(Action<>).MakeGenericType(typeof(EntityTypeBuilder<>).MakeGenericType(entityType));
                var configureAction = configureMethod.CreateDelegate(actionType, configInstance);

                buildMethod.Invoke(modelBuilder, new []{ configureAction });
            }
        }
    }
}