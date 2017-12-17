using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.View
{
    public class ViewManager
    {
        private ViewCompiler compiler;

        public ViewManager()
        {
            compiler = new ViewCompiler();
        }

        public void RegisterView(string ViewName, string ViewAlias)
        {
            compiler.RegisterTemplate(ViewName, ViewAlias);
        }

        public string Render(string ViewName)
        {
            return compiler.Render(ViewName);
        }

        public string Render(string ViewName, object ViewData, DynamicViewBag ViewBag = null)
        {
            return compiler.Render(ViewName, ViewData, ViewBag);
        }
    }
}
