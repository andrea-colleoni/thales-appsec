using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreLibrary.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private PersonaService personaService = new PersonaService();
        private IPersonaService _personaService;

        // uso la dependency injection di aspnet core
        public HomeController(ILogger<HomeController> logger, IPersonaService ps)
        {
            _logger = logger;
            _personaService = ps;
        }

        public IActionResult Index()
        {
            //ViewData["persona"] = p;
            ViewBag.Persona = _personaService.PersonaDiTest();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Persona()
        {
            _logger.LogInformation("lettura di una persona di test");
            return View(_personaService.PersonaDiTest());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
