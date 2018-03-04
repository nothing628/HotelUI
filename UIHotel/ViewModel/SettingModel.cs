using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

                    var dset = new DataTable();
                    var command = new MySqlCommand("SELECT MAX(Version) FROM versioninfo", conn);
                    var adapter = new MySqlDataAdapter(command);

                    adapter.Fill(dset);

                    var max = (long)dset.Rows[0][0];

                    if (max != 6) //TODO: Need to find max of latest migration
                    {
                        connOK = false;
                    }
                } catch
                {
                    connOK = false;
                }
            }

            return connOK;
        }
    }
}
