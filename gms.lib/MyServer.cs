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
