using System;
using System.Collections.Generic;
using System.IO;
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

            var pr = _s16693context.Projekt
                .Include(f => f.IdFirmaNavigation)
                    .ThenInclude(kf => kf.KlientFirma)
                        .ThenInclude(k => k.IdKlientNavigation)
                            .ThenInclude(o => o.IdKlientNavigation)
                .Where(x => x.IdFirmaNavigation.KlientFirma.Any(e => e.IdKlientNavigation.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value))
                .ToList();
            
            return View(pr);
        }

        public IActionResult Meetings()
        {
            var meet = _s16693context.PracownikKlient
                .Include(k => k.IdKlientNavigation)
                    .ThenInclude(o => o.IdKlientNavigation)
                .Include(p => p.IdPracownikNavigation)
                    .ThenInclude(po => po.IdPracownikNavigation)
                .Where(x => x.IdKlientNavigation.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                .ToList();
            return View(meet);
        }

        public IActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _s16693context.Projekt
            .FirstOrDefault(e => e.IdProjekt == id);

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return View(project);
            }


        }

        public IActionResult Team(int? id)
        {
            var team = _s16693context.ZespolProjekt
                .Include(p => p.IdProjektNavigation)
                .Include(z => z.IdZespolNavigation)
                .FirstOrDefault(x => x.IdProjekt == id);

            var members = _s16693context.PracownikZespol
                .Include(z => z.IdZespolNavigation)
                .Include(p => p.IdPracownikNavigation)
                    .ThenInclude(o => o.IdPracownikNavigation)
                    .Where(x => x.IdZespol == team.IdZespol)
                    .ToList();

            return View(members);

        }

        public IActionResult Contract(int? id)
        {
            var contract = _s16693context.ProjektPakiet
                .Include(pr => pr.IdProjektNavigation)
                .Include(pa => pa.IdPakietNavigation)
                .Where(x => x.IdProjekt == id)
                .OrderByDescending(e => e.IdPakiet)
                .ToList();

            return View(contract);
        }

        public IActionResult Profile()
        {
            var kl = _s16693context.Klient
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}