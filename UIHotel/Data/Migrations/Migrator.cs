using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.MySql;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Migrations
{
    class Migrator
    {
        private MigrationRunner GetRunner()
        {
            var filename = Process.GetCurrentProcess().MainModule.FileName;
            var fname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            var asm = Assembly.LoadFile(fname);
            var announcer = new NullAnnouncer();
            var context = new RunnerContext(announcer)
            {
                Database = "mysql",
                Timeout = 15,
                Connection = Properties.Settings.Default.MyDB,
                Namespace = "UIHotel.Data.Migrations",

            };
            var options = new ProcessorOptions() { Timeout = 15 };

            try
            {
                var factory = new MySqlDbFactory();
                var connection = new MySqlConnection(Properties.Settings.Default.MyDB);
                var generator = new MySqlGenerator();
                var processor = new MySqlProcessor(connection, generator, announcer, options, factory);
                var runner = new MigrationRunner(asm, context, processor);

                return runner;
            } catch
            {
                return null;
            }
            
        }

        public void RunDown()
        {
            try
            {
                var runner = GetRunner();

                runner.MigrateDown(0);
            } catch
            {
                //
            }
        }

        public void Run()
        {
            try
            {
                var runner = GetRunner();

                runner.MigrateUp(true);
            } catch
            {

            }
        }
    }
}
