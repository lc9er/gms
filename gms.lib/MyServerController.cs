using System.Configuration;
using System.Reflection;
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
                        WHERE {property} = '{value}' COLLATE NOCASE";

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

        public void AddMyServer(MyServer server)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using var tableCmd = connection.CreateCommand();
                connection.Open();

                if (!String.IsNullOrEmpty(server.Name) && !String.IsNullOrEmpty(server.FQDN))
                {
                    tableCmd.CommandText =
                        $@"INSERT INTO myservers ( FQDN, Name, IPAddress, Role, ENV, OperatingSystem, Status, Notes ) 
                            VALUES ( '{server.FQDN}', '{server.Name}', '{server.IPAddress}', 
                                     '{server.Role}', '{server.ENV}', '{server.OperatingSystem}', 
                                     '{server.Status}', '{server.Notes}' ) ";

                    tableCmd.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Name and unique FQDN required to add server.");
                }
            }
        }

        public void DeleteMyServer(MyServer server)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using var tableCmd = connection.CreateCommand();
                connection.Open();

                if (!String.IsNullOrEmpty(server.FQDN))
                {
                    tableCmd.CommandText =
                        $@"DELETE FROM myservers
                           WHERE FQDN = '{server.FQDN}' COLLATE NOCASE";
                    tableCmd.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Must provide FQDN.");
                }
            }
        }

        public void EditMyServer(MyServer server)
        {
            List<string> updateVals = GetUpdateString(server);

            //using (var connection = new SqliteConnection(connectionString))
            //{
            //    using var tableCmd = connection.CreateCommand();
            //    connection.Open();

            //    if (!String.IsNullOrEmpty(server.FQDN))
            //    {
            //        tableCmd.CommandText =
            //            $@"DELETE FROM myservers
            //               WHERE FQDN = '{server.FQDN}' COLLATE NOCASE";
            //        tableCmd.ExecuteNonQuery();
            //    }
            //    else
            //    {
            //        Console.WriteLine("Must provide FQDN.");
            //    }
            //}
        }

        public List<string> GetUpdateString(MyServer server)
        {
            Dictionary<string, string> editOpts = new();

            foreach (PropertyInfo prop in server.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    string propVal = (string)prop.GetValue(server, null);

                    if (!String.IsNullOrEmpty(propVal))
                    {
                        editOpts.Add(prop.Name, propVal);
                    }
                }
            }
            
            List<string> keys = editOpts.Keys.ToList();
            List<string> vals = editOpts.Values.ToList();

            string keyString = String.Join(", ", keys);
            string valString = String.Join(", ", vals);

            List<string> keysAndVals = new();
            keysAndVals.Add(keyString);
            keysAndVals.Add(valString);

            return keysAndVals;
        }
    }
}