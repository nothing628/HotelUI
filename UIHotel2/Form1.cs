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

namespace UIHotel2
{
    public partial class Form1 : Formium
    {
        public Form1() : base("http://assets.app.local/index.html")
        {
            InitializeComponent();

            GlobalObject.AddFunction("showDevTools").Execute += (func, args) => Chromium.ShowDevTools();

            var obj = GlobalObject.AddObject("CS");
            var prop = obj.AddDynamicProperty("new");

            var fun = obj.AddFunction("test2");
            fun.Execute += (_, args) => args.SetReturnValue("S");
        }
    }
}
