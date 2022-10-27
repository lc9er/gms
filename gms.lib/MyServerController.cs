using System.Configuration;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace gmslib
{
    public class MyServerController
    {

        private string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

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
                        WHERE [{property}] LIKE @PROPERTY COLLATE NOCASE";

                tableCmd.Parameters.AddWithValue("@PROPERTY", value);

                using var reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(new MyServer(reader));
                    }
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
                        $@"INSERT INTO myservers ( [FQDN], [Name], [IPAddress], [Role], [ENV], [OperatingSystem], [Status], [Notes] ) 
                            VALUES ( @FQDN, @Name, @IPAddress, 
                                     @Role, @ENV, @OperatingSystem, 
                                     @Status, @Notes )";

                    tableCmd.Parameters.AddWithValue("@FQDN", server.FQDN);
                    tableCmd.Parameters.AddWithValue("@Name", server.Name);
                    tableCmd.Parameters.AddWithValue("@IPAddress", server.IPAddress);
                    tableCmd.Parameters.AddWithValue("@Role", server.Role);
                    tableCmd.Parameters.AddWithValue("@ENV", server.ENV);
                    tableCmd.Parameters.AddWithValue("@OperatingSystem", server.OperatingSystem);
                    tableCmd.Parameters.AddWithValue("@Status", server.Status);
                    tableCmd.Parameters.AddWithValue("@Notes", server.Notes);
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
                        $"DELETE FROM myservers WHERE [FQDN] = @FQDN COLLATE NOCASE";
                    tableCmd.Parameters.AddWithValue("@FQDN", server.FQDN);
                    tableCmd.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Must provide FQDN.");
                }
            }
        }

        public void SetMyServer(MyServer editedServer)
        {
            List<MyServer> currentRecordList = new();
            currentRecordList = GetByProperty("FQDN", editedServer.FQDN);
            MyServer currentRecord = currentRecordList[0];

            currentRecord.MergeFrom(editedServer);

            using (var connection = new SqliteConnection(connectionString))
            {
                using var tableCmd = connection.CreateCommand();
                connection.Open();

                if (!String.IsNullOrEmpty(currentRecord.FQDN))
                {
                    tableCmd.CommandText = $@"UPDATE myservers
                                              SET 
                                                    [Name] = @Name,  [IPAddress] = @IPAddress, 
                                                    [Role] = @Role, [ENV] = @ENV, 
                                                    [OperatingSystem] = @OperatingSystem, 
                                                    [Status] = @Status, [Notes] = @Notes 
                                              WHERE [FQDN] = @FQDN";
                    tableCmd.Parameters.AddWithValue("@FQDN", currentRecord.FQDN);
                    tableCmd.Parameters.AddWithValue("@Name", currentRecord.Name);
                    tableCmd.Parameters.AddWithValue("@IPAddress", currentRecord.IPAddress);
                    tableCmd.Parameters.AddWithValue("@Role", currentRecord.Role);
                    tableCmd.Parameters.AddWithValue("@ENV", currentRecord.ENV);
                    tableCmd.Parameters.AddWithValue("@OperatingSystem", currentRecord.OperatingSystem);
                    tableCmd.Parameters.AddWithValue("@Status", currentRecord.Status);
                    tableCmd.Parameters.AddWithValue("@Notes", currentRecord.Notes);

                    tableCmd.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Must provide FQDN.");
                }
            }
        }
    }
}