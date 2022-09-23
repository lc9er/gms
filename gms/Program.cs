using System.Configuration;
using gmslib;

namespace gms
{
    class Program
    {
        static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new();
            databaseManager.CreateTable(connectionString);
        }
    }
}
