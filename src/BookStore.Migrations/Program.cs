using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BookStore.Migrations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = GetSettings(args, Directory.GetCurrentDirectory());

            var connectionString = options.ConnectionString;

            CreateDatabase(connectionString);

            var runner = CreateRunner(connectionString, options);

            runner.MigrateUp();
        }

        static void CreateDatabase(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            if (File.Exists(connectionStringBuilder.DataSource) == false)
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
        }

        static IMigrationRunner CreateRunner(string connectionString, MigrationSettings options)
        {
            var container = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Program).Assembly).For.All())
                .AddSingleton<MigrationSettings>(options)
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();
            return container.GetRequiredService<IMigrationRunner>();
        }

        private static MigrationSettings GetSettings(string[] args, string baseDir)
        {
            var configurations = new ConfigurationBuilder()
                .SetBasePath(baseDir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var settings = new MigrationSettings();
            configurations.Bind(settings);
            return settings;
        }
    }

    public class MigrationSettings
    {
        public string ConnectionString { get; set; }
    }

}
