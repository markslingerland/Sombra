using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging;

namespace Sombra.Infrastructure.DAL
{
    public abstract class SombraContext<T> : DbContext where T : SombraContext<T>, new()
    {
        private readonly bool _seed;
        private static SqliteConnection _sqliteConnection;

        protected SombraContext(DbContextOptions options, SombraContextOptions sombraContextOptions) : base(options)
        {
            _seed = sombraContextOptions.Seed;
        }

        protected SombraContext() : this(GetOptions(), new SombraContextOptions()) { }

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

        public Task<bool> TrySaveChangesAsync()
        {
            try
            {
                SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                Logger.LogExceptionAsync(ex);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}