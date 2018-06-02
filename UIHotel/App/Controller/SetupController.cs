using CefSharp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;
using UIHotel.App.Provider;
using UIHotel.Data.Migrations;
using UIHotel.Data.Seeds;

namespace UIHotel.App.Controller
{
    [AllowAuthorize]
    public class SetupController : BaseController
    {
        public SetupController(IRequest request) : base(request)
        {
            //
        }

        #region View
        public IResourceHandler index()
        {
            return View("Setup.Index");
        }

        public IResourceHandler migration()
        {
            return View("Setup.Migration");
        }

        public IResourceHandler appsetup()
        {
            return View("Setup.App");
        }

        public IResourceHandler finish()
        {
            return View("Setup.Finish");
        }
        #endregion

        #region API
        public IResourceHandler SaveSettingApp()
        {
            var tax = jToken.Value<int>("Tax");
            var deposit = jToken.Value<int>("Deposit");
            var pinalty = jToken.Value<int>("Pinalty");
            var hotel_name = jToken.Value<string>("HotelName");
            var hotel_logo = jToken.Value<string>("HotelLogo");
            var hotel_address = jToken.Value<string>("HotelAddress");
            var checkin_time = jToken.Value<string>("CheckinTime");
            var checkout_time = jToken.Value<string>("CheckoutTime");

            try
            {
                SettingProvider.CheckinTime = TimeSpan.Parse(checkin_time);
                SettingProvider.CheckoutTime = TimeSpan.Parse(checkout_time);
                SettingProvider.HotelName = hotel_name;
                SettingProvider.HotelLogo = hotel_logo;
                SettingProvider.HotelAddress = hotel_address;
                SettingProvider.Deposit = deposit;
                SettingProvider.Pinalty = pinalty;
                SettingProvider.TaxPercent = tax;
                SettingProvider.SaveDBSetting();

                return Json(new { success = true, message = "Setting Saved" });
            }
            catch
            {
                return Json(new { success = false, message = "Something Error" });
            }
        }

        public IResourceHandler SaveSettingDB()
        {
            try
            {
                SettingProvider.SQL_Database = jToken.Value<string>("SQL_Database");
                SettingProvider.SQL_Server = jToken.Value<string>("SQL_Server");
                SettingProvider.SQL_Port = jToken.Value<int>("SQL_Port");
                SettingProvider.SQL_User = jToken.Value<string>("SQL_User");
                SettingProvider.SQL_Password = jToken.Value<string>("SQL_Password");
                SettingProvider.SaveLocalSetting();

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public IResourceHandler DoMigration()
        {
            try
            {
                DropTables();
                MigrateTables();
                
                return Json(new
                {
                    success = true,
                    message = "Installing success"
                });
            } catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.ToString()
                });
            }
        }
        #endregion

        #region Misc
        private void MigrateTables()
        {
            var migrator = new Migrator();
            migrator.RunUp();
            DBSeeder.Seed();
        }

        private void DropTables()
        {
            var tables = GetTables();
            var qryStrs = (from a in tables
                           select "DROP TABLE IF EXISTS " + a).ToList();
            
            using (var conn = new MySqlConnection(SettingProvider.SQL_Connection_Str))
            {
                conn.Open();

                new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0", conn).ExecuteNonQuery();

                foreach (var command in qryStrs)
                {
                    var adapter = new MySqlCommand(command, conn);

                    adapter.ExecuteNonQuery();
                }

                new MySqlCommand("SET FOREIGN_KEY_CHECKS = 1", conn).ExecuteNonQuery();
            }
        }

        private List<string> GetTables()
        {
            var rest = new List<string>();
            var dset = new DataTable();
            var sqlCmd = @"
SELECT table_name
FROM information_schema.tables
WHERE table_schema = '" + SettingProvider.SQL_Database + "'";

            try
            {
                using (var conn = new MySqlConnection(SettingProvider.SQL_Connection_Str))
                using (var comm = new MySqlCommand(sqlCmd, conn))
                using (var adapter = new MySqlDataAdapter(comm))
                {
                    adapter.Fill(dset);

                    foreach (DataRow row in dset.Rows)
                    {
                        rest.Add((string)row[0]);
                    }
                }
            } catch
            {
                //
            }
            
            return rest;
        }
        #endregion
    }
}
