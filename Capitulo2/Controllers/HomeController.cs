using Abp.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error(int? statuscode = null) {
        //    if(statuscode.HasValue) {
        //        if (statuscode == 404 || statuscode == 500) {
        //            var viewname = $"Erro{statuscode.ToString()}";
        //            return View(viewname);
        //        }
        //    }
        //    return View(new ErrorViewModel {
        //        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        //    });
        //}
    }
}
