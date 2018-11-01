using Chromium.Remote.Event;
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
using UIHotel2.JsObject;

namespace UIHotel2
{
    public partial class Form1 : Formium
    {
        public Form1() : base("about:blank")
        {
            InitializeComponent();

            Text = "Hotel Management System";
            WindowState = FormWindowState.Maximized;
            GlobalObject.AddFunction("showDevTools").Execute += (func, args) => Chromium.ShowDevTools();
            GlobalObject.AddFunction("windowMinimize").Execute += (func, args) => WindowState = FormWindowState.Minimized;
            GlobalObject.AddFunction("windowMaximize").Execute += (func, args) => WindowState = (WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
            GlobalObject.AddFunction("windowClose").Execute += CloseExecute;
            Chromium.BrowserCreated += Chromium_BrowserCreated;
            RegisterObject();
            CallJavascriptO = new DelegateCallJavascript(CallJavascript);
        }

        private void CloseExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            Close();
        }

        private void CallJavascript()
        {
            ExecuteJavascript("alert('12');");
        }

        public delegate void DelegateCallJavascript();
        public DelegateCallJavascript CallJavascriptO;

        private void RegisterObject()
        {
            List<IBaseObject> listObject = new List<IBaseObject>();
            var repository = GlobalObject.AddObject("CS");

            listObject.Add(new AuthObject());
            listObject.Add(new AppObject(this));
            listObject.Add(new DBObject());
            listObject.Add(new HotelObject());
            listObject.Add(new SettingObject());

            repository.AddDynamicProperty("IsDebug").PropertyGet += IsDebug_PropertyGet;

            foreach (var obj in listObject)
            {
                obj.Register(repository);
            }
        }

        private void IsDebug_PropertyGet(object sender, Chromium.Remote.Event.CfrV8AccessorGetEventArgs e)
        {
#if DEBUG
            e.Retval = true;
#else
            e.Retval = false;
#endif
            e.SetReturnValue(true);
        }

        private void Chromium_BrowserCreated(object sender, Chromium.WebBrowser.Event.BrowserCreatedEventArgs e)
        {
#if DEBUG
            Chromium.ShowDevTools();
#endif
        }
    }
}
