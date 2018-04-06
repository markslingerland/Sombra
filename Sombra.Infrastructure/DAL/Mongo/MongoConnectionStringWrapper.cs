namespace Sombra.Infrastructure.DAL.Mongo
{
    public class MongoConnectionStringWrapper
    {
        public MongoConnectionStringWrapper()
        {
        }

        public MongoConnectionStringWrapper(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}