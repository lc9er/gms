namespace gms.lib
{
    public class MyServer
    {
        private string _name;
        private string _fqdn;
        private string _ip;
        private string _role;
        private string _env;
        private string _os;
        private string _notes;

        public MyServer()
        {

        }

        public MyServer(string Name, string FQDN, string IPAddress,
                        string Role, string ENV, string OperatingSystem,
                        string Notes)
        {
            _name = Name;
            _fqdn = FQDN;
            _ip = IPAddress;
            _role = Role;
            _env = ENV;
            _os = OperatingSystem;
            _notes = Notes;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string FQDN
        {
            get => _fqdn;
            set => _fqdn = value;
        }

        public string IPAddress
        {
            get => _ip;
            set => _ip = value;
        }

        public string Environment
        {
            get => _env;
            set => _env = value;
        }

        public string Role
        {
            get => _role;
            set => _role = value;
        }
        public string OperatingSystem
        {
            get => _os;
            set => _os = value;
        }
        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }
    }
}
