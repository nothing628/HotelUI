using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.View
{
    public class ViewNotFoundException : Exception
    {
        public string ViewName { get; set; }
        public string ExpectedPath { get; set; }

        public string GetMessage()
        {
            return "View name : '" + ViewName + "' is Not Found";
        }

        public ViewNotFoundException(string ViewName, string ExpectedPath)
        {
            this.ViewName = ViewName;
            this.ExpectedPath = ExpectedPath;
        }
    }
}
