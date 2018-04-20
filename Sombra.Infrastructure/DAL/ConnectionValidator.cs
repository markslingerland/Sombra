using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using MongoDB.Driver.Core.Clusters;
using Sombra.Core;
using Sombra.Infrastructure.DAL.Mongo;

namespace Sombra.Infrastructure.DAL
{
    public static class ConnectionValidator
    {
        public static void ValidateAllDbConnections(ServiceProvider serviceProvider)
        {
            var connectionStrings = serviceProvider.GetServices<SqlConnectionStringWrapper>();
            
            foreach (var connectionStringWrapper in connectionStrings)
            {
                var factory = SqlClientFactory.Instance;
                var connectionIsWorking = false;

                while (!connectionIsWorking)
                {
                    using (var connection = factory.CreateConnection())
                    {
                        connection.ConnectionString = connectionStringWrapper.ConnectionStringWithoutDatabase;
                        try
                        {
                            connection.Open();
                            if (connection.State == ConnectionState.Open)
                            {
                                connectionIsWorking = true;
                                connection.Close();
                            }
                        }
                        catch (SqlException e)
                        {
                            connectionIsWorking = false;
                            Thread.Sleep(2500);
                            ExtendedConsole.Log($"ConnectionValidator exception: {e}");
                            ExtendedConsole.Log($"Waiting for {connectionStringWrapper.ContextType.Name} to come online..");
                        }
                    }
                }

                ExtendedConsole.Log($"{connectionStringWrapper.ContextType.Name} is online.");
            }

            var mongoConnectionStrings = serviceProvider.GetServices<MongoConnectionStringWrapper>();
            foreach (var connectionStringWrapper in mongoConnectionStrings)
            {
                while (true)
                {
                    var client = new MongoClient(connectionStringWrapper.ConnectionString);
                    if (client.Cluster.Description.State == ClusterState.Connected) break;

                    Thread.Sleep(2500);
                    ExtendedConsole.Log($"Waiting for mongoserver for database {connectionStringWrapper.DatabaseName} to come online..");
                }
                
                ExtendedConsole.Log($"Mongoserver for database {connectionStringWrapper.DatabaseName} is online.");
            }
        }
    }
}