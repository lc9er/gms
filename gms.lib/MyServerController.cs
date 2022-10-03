using System.Configuration;
using Microsoft.Data.Sqlite;

namespace gmslib
{
    public class MyServerController
    {

        public string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public MyServerController(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public List<MyServer> GetAll()
        {
            List<MyServer> results = new();

            using (var connection = new SqliteConnection(connectionString))
            {
                using var tableCmd = connection.CreateCommand();
                connection.Open();
                tableCmd.CommandText = "SELECT * FROM myservers";

                using var reader = tableCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(new MyServer(reader));
                    }
                }
                else
                {
                    Console.WriteLine("No servers found!");
                }
            }

            return results;
        }
        
        public List<MyServer> GetByProperty(string property, string value)
        {
            List<MyServer> results = new List<MyServer>();

            using (var connection = new SqliteConnection(connectionString))
            {
                using var tableCmd = connection.CreateCommand();
                connection.Open();
                tableCmd.CommandText =
                    $@"SELECT * 
                        FROM myservers
                        WHERE {property} = '{value}'";

                using var reader = tableCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(new MyServer(reader));
                    }
                }

                else
                {
                    Console.WriteLine("No servers found!");
                }
            }

            return results;
        }
    }
}