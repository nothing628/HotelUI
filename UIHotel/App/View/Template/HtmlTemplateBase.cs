using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.View.Template
{
    public abstract class HtmlTemplateBase<T> : TemplateBase<T>
    {
        public HtmlHelper Html { get; set; }

        public HtmlTemplateBase()
        {
            Html = new HtmlHelper();
        }
    }
}
