using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace UIHotel
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser browser;

        public Form1()
        {
            InitializeComponent();
            browser = new ChromiumWebBrowser("http://www.google.com/");
            browser.Dock = DockStyle.Fill;
            browser.Parent = this;

            Controls.Add(browser);
        }
    }
}
