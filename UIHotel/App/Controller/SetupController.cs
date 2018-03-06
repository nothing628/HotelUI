using CefSharp;
using System;
using System.Collections.Generic;
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
            return Json(new { success = true });
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
                var migrator = new Migrator();
                migrator.RunDown();
                migrator.Run();
                DBSeeder.Seed();

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
        public bool DropTables()
        {
            return true;
        }
        #endregion
    }
}
