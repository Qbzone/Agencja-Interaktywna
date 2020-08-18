using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            ModelState.Remove(nameof(Osoba.CzyEmailZweryfikowane));
            ModelState.Remove(nameof(Osoba.KodAktywacyjny));
            bool Status = false;
            string Message = "";

            if (ModelState.IsValid)
            {
                
                var isExist = IsEmailExist(osoba.AdresEmail);

                if (isExist)
                {
                    ModelState.AddModelError("Email.Exist", "Email already exist");
                }

                osoba.KodAktywacyjny = Guid.NewGuid();

                osoba.Haslo = Hash(osoba.Haslo);

                osoba.CzyEmailZweryfikowane = false;

                using (s16693Context dc = new s16693Context)
                {
                    dc.Osoba.Add(osoba);
                    dc.SaveChanges();

                    SendVerificationLink(osoba.AdresEmail, osoba.KodAktywacyjny.ToString());
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

        [NonAction]
        public void SendVerificationLink(string Email, string Code)
        {
            var verifyUrl = "Osoba/VerifyAccount/" + Code;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

        }

        public static string Hash(string Value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Value)));
        }
        
    }
}
