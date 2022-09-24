using CommandLine;
using CommandLine.Text;

namespace gms
{
    public class Options
    {
        [Value(0, MetaName = "Server Name",
                HelpText = "Server Name",
                Required = false)]
        public string? name { get; set; }

        [Option('f', "fqdn",
            HelpText = "Fully Qualified Domain Name")]
        public string fqdn { get; set; }

        [Option('i', "ipaddress",
            HelpText = "IPAddress")]
        public string ipaddr { get; set; }

        [Option('e', "env",
            HelpText = "Environment")]
        public string env { get; set; }

        [Option('r', "role",
            HelpText = "Role")]
        public string role { get; set; }
        
        [Option('s', "status",
            HelpText = "Deployment Status")]
        public string status { get; set; }
        
        [Option('o', "OS",
            HelpText = "Operating System")]
        public string os { get; set; }

        [Option('n', "Notes",
            HelpText = "Server notes")]
        public string notes { get; set; }

        [Usage(ApplicationAlias = "csvd")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Get all servers.",
                    new Options {});
                yield return new Example("Get a server with the specified name.",
                    new Options { name = "SomeServer"});
            }
        }
    }
}
