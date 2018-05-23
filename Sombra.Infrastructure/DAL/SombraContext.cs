using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext<T> : DbContext where T : SombraContext<T>, new()
    {
        private readonly bool _seed;
        private static SqliteConnection _sqliteConnection;

        protected SombraContext(DbContextOptions options, bool seed = false) : base(options)
        {
            _seed = seed;
            if (Database.IsSqlServer()) Database.Migrate();
        }

        protected SombraContext() : this(GetOptions()) { }

        public static T GetInMemoryContext()
        {
            var context = new T();
            context.Database.EnsureCreated();

            return context;
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

        private static DbContextOptions<T> GetOptions()
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