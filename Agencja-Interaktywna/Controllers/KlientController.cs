using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Agencja_Interaktywna.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencja_Interaktywna.Controllers
{
    [Authorize(Roles = "Klient")]
    public class KlientController : Controller
    {
        private readonly s16693Context _s16693context = new s16693Context();
        public ActionResult Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var pr = _s16693context.Projekts
                .Include(f => f.IdFirmaNavigation)
                    .ThenInclude(kf => kf.KlientFirmas)
                        .ThenInclude(k => k.IdKlientNavigation)
                            .ThenInclude(o => o.IdKlientNavigation)
                .Where(x => x.IdFirmaNavigation.KlientFirmas.Any(e => e.IdKlientNavigation.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value))
                .ToList();
            
            return View(pr);
        }

        public IActionResult Meetings()
        {
            return View();
        }

        public IActionResult Teams()
        {
            return View();
        }

        public IActionResult Profile()
        {
            var kl = _s16693context.Klients
                .Include(o => o.IdKlientNavigation)
                .FirstOrDefault(i => i.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);
           
            if(kl == null)
            {
                return NotFound();
            }
            else
            {
                return View(kl);
            }

        }

        public IActionResult Contact()
        {
            return View();
        }

        private async Task<Osoba> GetCurrentUser()
        {
            return await _s16693context.GetUserAsync(HttpContext.User);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}