using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using POILibrary.Models;
using Microsoft.Extensions.Options;

namespace POILibrary.Controllers
{
    public class HomeController : Controller
    {
        public HomeSettings Settings {get; set;}

        public HomeController(IOptions<HomeSettings> options)
        {
            Settings = options.Value;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = Settings.Title + HttpContext.User.Identity.Name;
            return View();
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
