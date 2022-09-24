using System.Configuration;
using Microsoft.Data.Sqlite;

namespace gmslib
{
    public class MyServerController
    {

        public string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public MyServerController(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public List<MyServer> GetAll()
        {
            List<MyServer> results = new List<MyServer>();
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "SELECT * FROM myservers";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                results.Add(
                                        new MyServer
                                        {
                                            FQDN            = reader.GetString(0),
                                            Name            = reader.GetString(1),
                                            IPAddress       = reader.GetString(2),
                                            Role            = reader.GetString(3),
                                            ENV             = reader.GetString(4),
                                            OperatingSystem = reader.GetString(5),
                                            Status          = reader.GetString(6),
                                            Notes           = reader.GetString(7),
                                        });
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found!");
                        }
                    }
                }
            }

            return results;
        }
    }
}