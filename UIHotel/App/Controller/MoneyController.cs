﻿using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;

namespace UIHotel.App.Controller
{
    [Authorize(Auth.AuthLevel.Administrator)]
    [Authorize(Auth.AuthLevel.Manager)]
    public class MoneyController : BaseController
    {
        public MoneyController(IRequest request) : base (request)
        {
            //
        }

        public IResourceHandler transaksi()
        {
            return View("Money.Transaction");
        }

        public IResourceHandler graph()
        {
            return View("Money.Graph");
        }
    }
}
