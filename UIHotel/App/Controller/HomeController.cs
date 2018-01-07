﻿using CefSharp;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.Controller
{
    public class HomeController : BaseController
    {
        public HomeController(IRequest request) : base(request)
        {
            //
        }

        public IResourceHandler index()
        {
            return View("Index");
        }

        public IResourceHandler login()
        {
            return View("Login");
        }

        public IResourceHandler test()
        {
            var query = Query["foo"];
            var data = Query["data"];
            
            var obj = new { Nama = "A" };

            ViewBag.Name = "test";

            return View("Test", obj);
        }
    }
}
