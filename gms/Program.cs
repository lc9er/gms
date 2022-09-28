using CommandLine;
using CommandLine.Text;
using System.Configuration;
using System.Collections.Generic;
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

            var parser = new CommandLine.Parser(with => with.HelpWriter = null);
            var parserResults = parser.ParseArguments<GetOptions, AddOptions>(args);

            parserResults
                .WithParsed<GetOptions>(opts =>
                    {
                        RunGet(opts.name);
                    })
                .WithParsed<AddOptions>(opts =>
                    {
                        RunAdd(opts.name);
                    })
                .WithNotParsed(errs => DisplayHelp(parserResults, errs));
        }

        static void RunGet(string Name)
        {
            MyServerController myServerController = new(connectionString);
            List<MyServer> results = new();

            results = myServerController.GetAll();

            foreach (var item in results)
            {
                Console.WriteLine(item.Name);
            }
            
            results = myServerController.GetByName("lilserver");

            foreach (var item in results)
            {
                Console.WriteLine(item.Name);
            }
        }
       
        static void RunAdd(string Name)
        {
            // Placeholder
            Console.WriteLine(Name);
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
