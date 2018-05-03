using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext<T> : DbContext where T : SombraContext<T>, new()
    {
        private readonly bool _seed = true;
        private static SqliteConnection _sqliteConnection;

        protected SombraContext(DbContextOptions options) : base(options)
        {
            if (Database.IsSqlServer()) Database.Migrate();
        }

        protected SombraContext(DbContextOptions options, bool seed) : base(options)
        {
            _seed = seed;
            if (Database.IsSqlServer()) Database.Migrate();
        }

        public static T GetInMemoryContext()
        {
            return new T();
        }

        public static void OpenInMemoryConnection()
        {
            _sqliteConnection = new SqliteConnection("DataSource=:memory:");
            _sqliteConnection.Open();
        }

        public static void CloseInMemoryConnection()
        {
            if (_sqliteConnection == null)
                throw new NullReferenceException("A non-existing connection cannot be closed!");
            _sqliteConnection.Close();
            _sqliteConnection = null;
        }

        protected static DbContextOptions<T> GetOptions()
        {
            if (_sqliteConnection == null)
                throw new NullReferenceException($"You must call {nameof(OpenInMemoryConnection)} before creating an in-memory database context!");

            if (_sqliteConnection.State != ConnectionState.Open)
                throw new InvalidOperationException($"You must call {nameof(OpenInMemoryConnection)} before creating an in-memory database context!");

            return new DbContextOptionsBuilder<T>()
                .UseSqlite(_sqliteConnection)
                .Options;
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