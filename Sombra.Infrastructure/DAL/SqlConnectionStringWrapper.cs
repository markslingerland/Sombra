using System;

namespace Sombra.Infrastructure.DAL
{
    public class SqlConnectionStringWrapper
    {
        public SqlConnectionStringWrapper()
        {
        }

        public SqlConnectionStringWrapper(string connectionString, Type contextType)
        {
            ConnectionString = connectionString;
            ContextType = contextType;
        }

        public string ConnectionString { get; set; }
        public Type ContextType { get; set; }
    }
}