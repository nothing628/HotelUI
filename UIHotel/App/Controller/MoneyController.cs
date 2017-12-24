using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.Controller
{
    public class MoneyController : BaseController
    {
        public MoneyController(IRequest request) : base (request)
        {
            //
        }

        public IResourceHandler index()
        {
            return View("Money.List");
        }

        public IResourceHandler pengeluaran()
        {
            return View("Money.Out");
        }

        public IResourceHandler pemasukkan()
        {
            return View("Money.In");
        }
    }
}
