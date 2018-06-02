using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.MySql;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UIHotel.App.Provider;
using System.Linq;
using Serilog.Core;
using Serilog;

namespace UIHotel.Data.Migrations
{
    class Migrator
    {
        /// <summary>
        /// Configure the dependency injection services
        /// </sumamry>
        private IServiceProvider CreateServices()
        {
            var filename = Process.GetCurrentProcess().MainModule.FileName;
            var fname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            var asm = Assembly.LoadFile(fname);
            var conn_str = SettingProvider.SQL_Connection_Str;

            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddMySql5()
                    // Set the connection string
                    .WithGlobalConnectionString(conn_str)
                    // Define the assembly containing the migrations
                    .WithMigrationsIn(asm))
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Rollback all database
        /// </summary>
        public void RunDown()
        {
            try
            {
                // Instantiate the runner
                var serviceProvider = CreateServices();
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateDown(0);
            } catch (Exception ex)
            {
                Log.Error(ex, "Error Migrating Down");
            }
        }

        /// <summary>
        /// Migrating Database
        /// </summary>
        public void RunUp()
        {
            try
            {
                // Instantiate the runner
                var serviceProvider = CreateServices();
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateUp();
            } catch (Exception ex)
            {
                Log.Error(ex, "Error Migrating Up");
            }
        }
    }
}
