using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Agencja_Interaktywna.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencja_Interaktywna.Controllers
{
    [Authorize(Roles = "Programista")]
    public class ProgramistaController : Controller
    {
        private readonly s16693Context _s16693context = new s16693Context();
        public IActionResult Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

           /* var zespol = _s16693context.Zespol
                    .Include(pz => pz.PracownikZespol)
                        .ThenInclude(p => p.IdPracownikNavigation)
                            .ThenInclude(o => o.IdPracownikNavigation)
                    .Where(x => x.PracownikZespol.Any(e => e.IdPracownikNavigation.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value))
                    .ToList();*/



                var pr = _s16693context.Projekt
                    .Include(zp => zp.ZespolProjekt)
                        .ThenInclude(z => z.IdZespolNavigation)
                            .ThenInclude(z => z.PracownikZespol)
                                .ThenInclude(p => p.IdPracownikNavigation)
                                    .ThenInclude(o => o.IdPracownikNavigation)
                    .Where(x => x.ZespolProjekt.Any(x => x.IdZespolNavigation.PracownikZespol
                    .Any(e => e.IdPracownikNavigation.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value) && x.DataWypisaniaZespolu != null))
                    .ToList();

            
            
            return View(pr);
        }

        public IActionResult Profile()
        {
            var pr = _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .FirstOrDefault(i => i.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            if (pr == null)
            {
                return NotFound();
            }
            else
            {
                return View(pr);
            }

        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}