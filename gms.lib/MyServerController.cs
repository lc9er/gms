namespace gmslib
{
    public class MyServerController
    {

        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

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
                                            Name            = reader.GetString(1),
                                            FQDN            = reader.GetString(2),
                                            IPAddress       = reader.GetString(3),
                                            Role            = reader.GetString(4),
                                            ENV             = reader.GetString(5),
                                            OperatingSystem = reader.GetString(6),
                                            Status          = reader.GetString(7),
                                            Notes           = reader.GetString(8),
                                        });
                            }
                        }
                    }
                }
            }

            return results;
        }

    }
}