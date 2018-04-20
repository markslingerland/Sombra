using System;
using System.Collections.Generic;
using System.Linq;

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
        public string ConnectionStringWithoutDatabase => GetConnectionStringWithoutDatabase();

        private string _connectionStringWithoutDatabase;
        private readonly Dictionary<string, string> _connectionParameters = new Dictionary<string, string>();

        private string GetConnectionStringWithoutDatabase()
        {
            if (!string.IsNullOrEmpty(_connectionStringWithoutDatabase)) return _connectionStringWithoutDatabase;
            if (_connectionParameters.Count > 0)
            {
                _connectionStringWithoutDatabase = _connectionParameters
                    .Where(p => !p.Key.Equals("Database", StringComparison.OrdinalIgnoreCase))
                    .Aggregate("", (result, kv) => $"{result}{kv.Key}={kv.Value};");
                return GetConnectionStringWithoutDatabase();
            }

            var parameters = ConnectionString.Split(';').Where(p => !string.IsNullOrEmpty(p));
            foreach (var parameter in parameters)
            {
                var kvPair = parameter.Split('=');
                _connectionParameters.Add(kvPair[0], kvPair[1]);
            }
            return GetConnectionStringWithoutDatabase();
        }
    }
}