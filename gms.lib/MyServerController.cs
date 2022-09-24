namespace gmslib
{
    public class MyServerController
    {

        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public List<MyServer> GetAll()
        {
            var results = new List<MyServer>();

            public void Get()
            {
                List<MyServer> tableData = new List<MyServer>();
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
                                    tableData.Add(
                                            new MyServer
                                            {
                                            Name = reader.GetString(1),
                                            FQDN = reader.GetString(2),
                                            Duration = reader.GetString(3)
                                            });
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n\nNo rows found.");
                            }
                        }
                    }

                    Console.WriteLine("\n\n");
                }

                TableVisualisation.ShowTable(tableData);
            }

        }
    }
}
