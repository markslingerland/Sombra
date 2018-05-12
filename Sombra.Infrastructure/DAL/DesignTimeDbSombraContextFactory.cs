using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sombra.Infrastructure.DAL
{
    public abstract class DesignTimeDbSombraContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : SombraContext<TContext>, new()
    {
        protected abstract string ConnectionStringName { get; }

        public TContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TContext>();
            var connectionString = configuration[ConnectionStringName];
            builder.UseSqlServer(connectionString);

            return (TContext) Activator.CreateInstance(typeof(TContext), builder.Options);
        }
    }
}