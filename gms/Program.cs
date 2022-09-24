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
            var parserResults = parser.ParseArguments<Options>(args);
            parserResults
                .WithParsed<Options>(opts =>
                    {
                        Run(opts.name);
                    })
            .WithNotParsed(errs => DisplayHelp(parserResults, errs));
        }

        static void Run(string Name)
        {
            MyServerController myServerController = new();
            List<MyServer> results = new List<MyServer>();

            results = myServerController.GetAll();

            foreach (var item in results)
            {
                Console.WriteLine(item.Name);
            }
        }
        
        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "gms 0.0.1";
                h.Copyright = "Copyright (c) 2022 lc9er";
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
        }
    }
}
