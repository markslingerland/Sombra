using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using MongoDB.Driver;
using Sombra.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Sombra.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDatabase(this IServiceCollection services, string connectionString, string databaseName)
        {
            return services.AddTransient(provider => new MongoClient(connectionString).GetDatabase(databaseName));
        }

        public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, string connectionString)
            where TContext : SombraContext
        {
            return services.AddDbContext<TContext>(builders => builders.UseSqlServer(connectionString), ServiceLifetime.Transient);
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(null, AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> additionalInitAction)
        {
            return services.AddAutoMapper(additionalInitAction, AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> additionalInitAction, DependencyContext dependencyContext)
        {
            return services.AddAutoMapper(additionalInitAction, AppDomain.CurrentDomain.GetAssemblies());
        }

        private static readonly Action<IMapperConfigurationExpression> DefaultConfig = cfg => { };

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
            => AddAutoMapperClasses(services, null, assemblies);

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> additionalInitAction, params Assembly[] assemblies)
            => AddAutoMapperClasses(services, additionalInitAction, assemblies);

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> additionalInitAction, IEnumerable<Assembly> assemblies)
            => AddAutoMapperClasses(services, additionalInitAction, assemblies);

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IEnumerable<Assembly> assemblies)
            => AddAutoMapperClasses(services, null, assemblies);

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, params Type[] profileAssemblyMarkerTypes)
        {
            return AddAutoMapperClasses(services, null, profileAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly));
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> additionalInitAction, params Type[] profileAssemblyMarkerTypes)
        {
            return AddAutoMapperClasses(services, additionalInitAction, profileAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly));
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> additionalInitAction, IEnumerable<Type> profileAssemblyMarkerTypes)
        {
            return AddAutoMapperClasses(services, additionalInitAction, profileAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly));
        }


        private static IServiceCollection AddAutoMapperClasses(IServiceCollection services, Action<IMapperConfigurationExpression> additionalInitAction, IEnumerable<Assembly> assembliesToScan)
        {
            additionalInitAction = additionalInitAction ?? DefaultConfig;
            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();

            var allTypes = assembliesToScan
                .Where(a => a.GetName().Name != nameof(AutoMapper))
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            var profiles = allTypes
                .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();


            void ConfigAction(IMapperConfigurationExpression cfg)
            {
                additionalInitAction(cfg);

                foreach (var profile in profiles.Select(t => t.AsType()))
                {
                    cfg.AddProfile(profile);
                }
            }

            Mapper.Initialize(ConfigAction);
            var config = Mapper.Configuration;
            config.AssertConfigurationIsValid();

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IMappingAction<,>)
            };
            foreach (var type in openTypes.SelectMany(openType => allTypes
                .Where(t => t.IsClass
                    && !t.IsAbstract
                    && t.AsType().ImplementsGenericInterface(openType))))
            {
                services.AddTransient(type.AsType());
            }

            services.AddSingleton(config);
            return services.AddSingleton<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
        }

        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            return type.IsGenericType(interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => @interface.IsGenericType(interfaceType));
        }

        private static bool IsGenericType(this Type type, Type genericType)
            => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}