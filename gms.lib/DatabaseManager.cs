using Microsoft.Data.Sqlite;

namespace gmslib
{
    public class DatabaseManager
    {
        public void CreateTable (string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        @"CREATE TABLE IF NOT EXISTS myservers (
                                FQDN TEXT PRIMARY KEY,
                                Name TEXT,
                                IPAddress TEXT,
                                Role TEXT,
                                ENV TEXT,
                                OperatingSystem TEXT,
                                Status TEXT,
                                Notes TEXT
                                )";

                    tableCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
