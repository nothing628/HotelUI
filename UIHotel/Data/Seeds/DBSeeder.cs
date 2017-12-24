using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data;

namespace UIHotel.Data.Seeds
{
    public abstract class DBSeeder
    {
        public abstract void Run(DataContext context);

        public static void Seed()
        {
            using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.MyDB))
            using (DataContext context = new DataContext(connection, false))
            {
                new RoomSeeder().Run(context);
                new RoomStatusSeeder().Run(context);
                new InvoiceSeeder().Run(context);

                context.SaveChanges();
            }
        }
    }
}
