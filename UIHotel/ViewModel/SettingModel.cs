using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.ViewModel
{
    public class SettingModel
    {
        public object CheckDatabase(string host, int port, string username, string password, string dbname)
        {
            var setup = Properties.Settings.Default.MyDB;
            var connStr = string.Format(setup, host, username, password, port, dbname);
            var connOK = true;

            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                } catch
                {
                    connOK = false;
                }
            }

            return connOK;
        }
    }
}
