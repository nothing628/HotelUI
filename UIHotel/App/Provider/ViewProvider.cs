using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.View;

namespace UIHotel.App.Provider
{
    public class ViewProvider : ServiceProvider
    {
        public ViewManager ViewManager { get; set; }

        public override void Boot()
        {
        }

        public override string Provide()
        {
            return "view";
        }

        public override void Register()
        {
            ViewManager = new ViewManager();
        }
    }
}
