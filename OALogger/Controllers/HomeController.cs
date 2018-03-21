using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.OALogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OALogger.Models;

namespace OALogger.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFooService _fooService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IFooService fooService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _fooService = fooService;
        }

        public IActionResult Index()
        {
            _logger.LogWarning("Text Log Test in Web Project!");
            _fooService.DoSomethingWithLogging();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
