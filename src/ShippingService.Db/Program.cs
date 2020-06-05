using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DbUp;
using ShippingService.Db.Options;
using Microsoft.Extensions.Configuration;

namespace ShippingService.Db
{
    class Program
    {
        private static Regex _runPattern = new Regex(@".*");

        public static int Main(string[] args)
        {
            var configuration = LoadAppConfiguration();
            var upgradeOptions = configuration.GetSection("UpgradeOptions").Get<UpgradeOptions>();
            SetRunPattern(upgradeOptions.RunPattern);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(configuration.GetConnectionString("PostgresSqlDb"))
                .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly(), (fileName) => _runPattern.IsMatch(fileName))
                .WithExecutionTimeout(TimeSpan.FromSeconds(upgradeOptions.CommandExecutionTimeoutSeconds))
                .WithTransaction()
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                #if DEBUG
                Console.ReadLine();
                #endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }

        private static void SetRunPattern(string runPattern)
        {
            if (string.IsNullOrWhiteSpace(runPattern))
            {
                return;
            }

            _runPattern = new Regex(runPattern);
        }

        private static IConfigurationRoot LoadAppConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build();
        }
    }
}
