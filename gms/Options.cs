using CommandLine;
using CommandLine.Text;

namespace gms
{
    public class IOptions
    {
        [Option('n', "name", HelpText = "Server Name")]
        public string? name { get; set; }

        [Option('f', "fqdn", HelpText = "Fully Qualified Domain Name")]
        public string? fqdn { get; set; }

        [Option('i', "ipaddress", HelpText = "IPAddress")]
        public string? ipaddr { get; set; }

        [Option('e', "env", HelpText = "Environment")]
        public string? env { get; set; }

        [Option('r', "role", HelpText = "Role")]
        public string? role { get; set; }
        
        [Option('s', "status", HelpText = "Deployment Status")]
        public string? status { get; set; }
        
        [Option('o', "OS", HelpText = "Operating System")]
        public string? os { get; set; }

        [Option('d', "Description", HelpText = "Server notes and/or description")]
        public string? notes { get; set; }

    }

    [Verb("get", true, HelpText = "Get server(s) by <Property>. Returns all servers, by default.")]
    class GetOptions : IOptions
    {
        public string? name       { get; set; }
        public string? fqdn       { get; set; }
        public string? ipaddr     { get; set; }
        public string? env        { get; set; }
        public string? role       { get; set; }
        public string? status     { get; set; }
        public string? os         { get; set; }
        public string? description{ get; set; }
    }

    [Verb("add", false, HelpText = "Add a server to the database. Name and FQDN required.")]
    class AddOptions : IOptions
    {
        public string? name       { get; set; }
        public string? fqdn       { get; set; }
        public string? ipaddr     { get; set; }
        public string? env        { get; set; }
        public string? role       { get; set; }
        public string? status     { get; set; }
        public string? os         { get; set; }
        public string? description{ get; set; }
    }
}
