using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Agencja_Interaktywna.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencja_Interaktywna.Controllers
{
    [Authorize(Roles = "Klient")]
    public class KlientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Meetings()
        {
            return View();
        }

        public IActionResult Profile()
        {
            using (s16693Context context = new s16693Context())
            {
                var klient = context.Klient.FirstOrDefaultAsync(e => e.IdklientNavigation.AdresEmail == ClaimTypes.Name);

                if (klient == null)
                {
                    return NotFound();
                }

                return View(klient);
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}