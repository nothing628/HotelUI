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
        public void Run()
        {
            var filename = Process.GetCurrentProcess().MainModule.FileName;
            var fname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            var asm = Assembly.LoadFile(fname);
            NullAnnouncer announcer = new NullAnnouncer();
            RunnerContext context = new RunnerContext(announcer) {
                Database = "mysql",
                Timeout = 15,
                Connection = Properties.Settings.Default.MyDB,
                Namespace = "UIHotel.Data.Migrations",
                
            };
            ProcessorOptions options = new ProcessorOptions() { Timeout = 15 };
            
            MySqlDbFactory factory = new MySqlDbFactory();
            MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.MyDB);
            MySqlGenerator generator = new MySqlGenerator();
            MySqlProcessor processor = new MySqlProcessor(connection, generator, announcer, options, factory);
            MigrationRunner runner = new MigrationRunner(asm, context, processor);

            runner.MigrateUp(true);
        }
    }
}
