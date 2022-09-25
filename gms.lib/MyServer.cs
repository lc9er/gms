using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace gmslib
{
    public class MyServer
    {
        public string Name            { get; set; }
        public string FQDN            { get; set; }
        public string IPAddress       { get; set; }
        public string Role            { get; set; }
        public string ENV             { get; set; }
        public string OperatingSystem { get; set; }
        public string Status          { get; set; }
        public string Notes           { get; set; }

        public MyServer()
        {

        }

        public MyServer(SqliteDataReader reader)
        {
            FQDN            = reader.GetString(0);
            Name            = reader.GetString(1);
            IPAddress       = reader.GetString(2);
            Role            = reader.GetString(3);
            ENV             = reader.GetString(4);
            OperatingSystem = reader.GetString(5);
            Status          = reader.GetString(6);
            Notes           = reader.GetString(7);
        }

        public MyServer(string name, string fqdn, string ipaddr,
                        string role, string env, string os,
                        string status, string notes)
        {
            Name            = name;
            FQDN            = fqdn;
            IPAddress       = ipaddr;
            Role            = role;
            ENV             = env;
            OperatingSystem = os;
            Status          = status;
            Notes           = notes;
        }
    }
}
