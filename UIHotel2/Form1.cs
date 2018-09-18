using Chromium.WebBrowser;
using NetDimension.NanUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHotel2.AppObject;

namespace UIHotel2
{
    public partial class Form1 : Formium
    {
        public Form1() : base("http://assets.app.local/index.html")
        {
            InitializeComponent();

            Text = "Hotel Management System";
            WindowState = FormWindowState.Maximized;
            GlobalObject.AddFunction("showDevTools").Execute += (func, args) => Chromium.ShowDevTools();
            GlobalObject.AddFunction("windowMinimize").Execute += (func, args) => WindowState = FormWindowState.Minimized;
            GlobalObject.AddFunction("windowMaximize").Execute += (func, args) => WindowState = (WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
            GlobalObject.AddFunction("windowClose").Execute += (func, args) => Close();
            Chromium.BrowserCreated += Chromium_BrowserCreated;
            RegisterObject();
        }

        private void RegisterObject()
        {
            List<IBaseObject> listObject = new List<IBaseObject>();
            var repository = GlobalObject.AddObject("CS");

            listObject.Add(new AuthObject());
            listObject.Add(new DBObject());
            listObject.Add(new SettingObject());

            foreach (var obj in listObject)
            {
                obj.Register(repository);
            }
        }

        private void Chromium_BrowserCreated(object sender, Chromium.WebBrowser.Event.BrowserCreatedEventArgs e)
        {
#if DEBUG
            Chromium.ShowDevTools();
#endif
        }
    }
}
