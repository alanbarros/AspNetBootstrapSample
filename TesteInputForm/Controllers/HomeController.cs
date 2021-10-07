using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteInputForm.Models;

namespace TesteInputForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var credentials = GetSessionCredentials();

            return View();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult Login([FromForm] Credencial data)
        {
            HttpContext.Session.Set("token", Encoding.UTF8.GetBytes(data.Token));
            HttpContext.Session.Set("funcional", Encoding.UTF8.GetBytes(data.Funcional));

            return RedirectToAction(nameof(Privacy));
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

        private Credencial GetSessionCredentials()
        {

            if (HttpContext.Session.TryGetValue("token", out byte[] tokenB)
                && HttpContext.Session.TryGetValue("funcional", out byte[] funcional))
            {
                return new Credencial
                {
                    Token = Encoding.UTF8.GetString(tokenB),
                    Funcional = Encoding.UTF8.GetString(funcional)
                };
            };

            return null;
        }
    }

    public class Credencial
    {
        public string Token { get; set; }
        public string Funcional { get; set; }
    }
}
