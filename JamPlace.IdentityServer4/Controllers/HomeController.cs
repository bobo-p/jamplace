using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JamPlace.IdentityServer4.Models;
using JamPlace.IdentityServer4.Models.AppConfigModels;

namespace JamPlace.IdentityServer4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Urls _urls;

        public HomeController(ILogger<HomeController> logger, Urls urls)
        {
            _logger = logger;
            _urls = urls;
        }

        public IActionResult Index()
        {
            //return View();
            return Redirect(_urls.AppUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
