using CommandLine;
using CommandLine.Text;

namespace gms
{
    interface IOptions
    {
        [Option('n', "name", HelpText = "Server Name")]
        string? name { get; set; }

        [Option('f', "fqdn", HelpText = "Fully Qualified Domain Name")]
        string? fqdn { get; set; }

        [Option('i', "ipaddress", HelpText = "IPAddress")]
        string? ipaddr { get; set; }

        [Option('e', "env", HelpText = "Environment")]
        string? env { get; set; }

        [Option('r', "role", HelpText = "Role")]
        string? role { get; set; }
        
        [Option('s', "status", HelpText = "Deployment Status")]
        string? status { get; set; }
        
        [Option('o', "OS", HelpText = "Operating System")]
        string? os { get; set; }

        [Option('d', "Description", HelpText = "Server notes and/or description")]
        string? notes { get; set; }

    }

    [Verb("get", false, HelpText = "Returns all servers (default), or get servers by PROPERTY. Supports SQL-style wildcards (%).")]
    class GetOptions : IOptions
    {
        public string? name  { get; set; }
        public string? fqdn  { get; set; }
        public string? ipaddr{ get; set; }
        public string? env   { get; set; }
        public string? role  { get; set; }
        public string? status{ get; set; }
        public string? os    { get; set; }
        public string? notes { get; set; }
    }

    [Verb("add", false, HelpText = "Add a server to the database. Name and FQDN required.")]
    class AddOptions : IOptions
    {
        public string? name  { get; set; }
        public string? fqdn  { get; set; }
        public string? ipaddr{ get; set; }
        public string? env   { get; set; }
        public string? role  { get; set; }
        public string? status{ get; set; }
        public string? os    { get; set; }
        public string? notes { get; set; }
    }

    [Verb("set", false, HelpText = "Set a server record by FQDN.")]
    class SetOptions : IOptions
    {
        public string? name  { get; set; }
        public string? fqdn  { get; set; }
        public string? ipaddr{ get; set; }
        public string? env   { get; set; }
        public string? role  { get; set; }
        public string? status{ get; set; }
        public string? os    { get; set; }
        public string? notes { get; set; }

    }

    [Verb("del", false, HelpText = "Delete a server record by FQDN.")]
    class DelOptions
    {
        [Option('f', "fqdn", HelpText = "Fully Qualified Domain Name")]
        public string? fqdn { get; set; }
    }
}
