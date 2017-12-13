using CefSharp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data;

namespace UIHotel.App.Controller
{
    public class BaseController
    {
        private DataContext Model { get; set; }
        private MySqlConnection DBConnection { get; set; }
        private IPostData PostData { get; set; }
        private IRequest Request { get; set; }

        public BaseController()
        {
            //
        }

        public BaseController(IRequest Request)
        {
            this.Request = Request;
            this.DBConnection = new MySqlConnection(Properties.Settings.Default.MyDB);
            this.Model = new DataContext(DBConnection, true);

            DBConnection.Open();
        }
    }
}
