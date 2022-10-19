using CommandLine;
using CommandLine.Text;
using System.Configuration;
using Spectre.Console;
using gmslib;

namespace gms
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MyServer"].ConnectionString
            .Replace("%AppData%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new();
            databaseManager.CreateTable(connectionString);

            var parser = new CommandLine.Parser(with => with.HelpWriter = null);
            var parserResults = parser.ParseArguments<GetOptions, AddOptions, EditOptions, DelOptions>(args);

            parserResults
                .WithParsed<GetOptions>(opts =>
                    {
                        RunGet(opts);
                    })
                .WithParsed<AddOptions>(opts =>
                    {
                        RunAdd(opts);
                    })
                .WithParsed<EditOptions>(opts =>
                    {
                        RunEdit(opts);
                    })
                .WithParsed<DelOptions>(opts =>
                    {
                        RunDel(opts);
                    })
                .WithNotParsed(errs => DisplayHelp(parserResults, errs));
        }

        static void RunGet(GetOptions opts)
        {
            MyServerController myServerController = new(connectionString);
            List<MyServer> results = new();

            // new function to parse opts and clean this up
            if (!String.IsNullOrEmpty(opts.name))
            {
                results = myServerController.GetByProperty("Name", opts.name);
            }
            else if (!String.IsNullOrEmpty(opts.fqdn))
            {
                results = myServerController.GetByProperty("FQDN", opts.fqdn);
            }
            else if (!String.IsNullOrEmpty(opts.ipaddr))
            {
                results = myServerController.GetByProperty("IPAddress", opts.ipaddr);
            }
            else if (!String.IsNullOrEmpty(opts.env))
            {
                results = myServerController.GetByProperty("ENV", opts.env);
            }
            else if (!String.IsNullOrEmpty(opts.role))
            {
                results = myServerController.GetByProperty("Role", opts.role);
            }
            else if (!String.IsNullOrEmpty(opts.status))
            {
                results = myServerController.GetByProperty("status", opts.status);
            }
            else if (!String.IsNullOrEmpty(opts.os))
            {
                results = myServerController.GetByProperty("OperatingSystem", opts.os);
            }
            else if (!String.IsNullOrEmpty(opts.notes))
            {
                results = myServerController.GetByProperty("Notes", opts.notes);
            }
            else
            {
                results = myServerController.GetAll();
            }

            PrintOutput(results);
        }

        static void RunAdd(AddOptions opts)
        {
            MyServer myServer = new MyServer(opts.name, opts.fqdn, opts.ipaddr, 
                                             opts.role, opts.env, opts.os, 
                                             opts.status, opts.notes);

            MyServerController myServerController = new(connectionString);
            myServerController.AddMyServer(myServer);
        }

        static void RunDel(DelOptions opts)
        {
            MyServer myServer = new MyServer();
            myServer.FQDN = opts.fqdn;

            MyServerController myServerController = new(connectionString);
            myServerController.DeleteMyServer(myServer);
        }

        static void RunEdit(EditOptions opts)
        {
            MyServer myServer = new MyServer(opts.name, opts.fqdn, opts.ipaddr, 
                                             opts.role, opts.env, opts.os, 
                                             opts.status, opts.notes);

            MyServerController myServerController = new(connectionString);
            myServerController.EditMyServer(myServer);
        }

        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "gms 0.0.1";
                h.Copyright = "Copyright (c) 2022 lc9er";
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, 
            e => e,
            verbsIndex:true);
            Console.WriteLine(helpText);
        }

        static void PrintOutput(List<MyServer> servers)
        {
            string[] header = { "Name", "FQDN", "IPAddress", "Role", "Env", "OS", "Status", "Notes" };
            Table table = new Table();

            table.AddColumns(header);
            table.Border = TableBorder.Simple;

            foreach (var server in servers)
            {
                string[] row = { server.Name, server.FQDN, server.IPAddress,
                                 server.Role, server.ENV, server.OperatingSystem,
                                 server.Status, server.Notes };
                table.AddRow(row);
            }

            AnsiConsole.Write(table);
        }
    }
}
