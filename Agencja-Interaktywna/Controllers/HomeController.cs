using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Agencja_Interaktywna.Models;

namespace Agencja_Interaktywna.Controllers
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
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Osoba osoba)
        {
            bool Status = false;
            string Message = "";

            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(osoba.AdresEmail);

                if (isExist)
                {
                    ModelState.AddModelError("Email.Exist", "Email already exist");
                }
            }
            else
            {
                Message = "Invalid Request";
            }

            return View(osoba);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public bool IsEmailExist(string Email)
        {
            using (s16693Context dc = new s16693Context())
            {
                var check = dc.Osoba.Where(e => e.AdresEmail == Email).FirstOrDefault();

                return check != null;
            }
        }
    }
}
