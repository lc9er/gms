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
        }
    }
}
