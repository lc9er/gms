using CommandLine;
using CommandLine.Text;
using System.Configuration;
using System.Collections.Generic;
using gmslib;
using System.ComponentModel.Design;

namespace gms
{
    class Program
    {
        static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        //static readonly string[] verbOpts = ["name", "fqdn", "ipaddr", "env", "role", "status", "os", "notes"];

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

            foreach (var item in results)
            {
                Console.WriteLine(item.Name);
            }
        }

        static void RunAdd(AddOptions opts)
        {
            // Placeholder
            //MyServerController myServerController = new(connectionString);
            //List<MyServer> results = new();

            //myServerController.AddServer(opts.name, opts.fqdn, opts.ipaddr, opts.env, opts.role, opts.status, opts.os, opts.notes);
            //results = myServerController.GetByProperty("FQDN", opts.fqdn);
        }

        static void RunDel(DelOptions opts)
        {

        }

        static void RunEdit(EditOptions opts)
        {
            
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
    }
}
