using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Agencja_Interaktywna.Models;
using Agencja_Interaktywna.Models.Functional;
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
        public async Task<IActionResult> Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var pr = await _s16693context.Projekt
                .Include(f => f.IdFirmaNavigation)
                    .ThenInclude(kf => kf.KlientFirma)
                        .ThenInclude(k => k.IdKlientNavigation)
                            .ThenInclude(o => o.IdKlientNavigation)
                .Where(x => x.IdFirmaNavigation.KlientFirma.Any(e => e.IdKlientNavigation.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value))
                .ToListAsync();
            
            var kf = await _s16693context.KlientFirma
                .Include(k => k.IdKlientNavigation)
                    .ThenInclude(o => o.IdKlientNavigation)
                .FirstOrDefaultAsync(e => e.IdKlientNavigation.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            if (kf != null)
            {
                return View(pr);
            }
            else
            {
                return RedirectToAction(nameof(AssignCompany));
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> AssignCompany()
        {
            var kl = await _s16693context.Klient
                .Include(o => o.IdKlientNavigation)
                    .FirstOrDefaultAsync(e => e.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);
            
            var aCM = new AssignCompanyModel
            {
                Klient = kl
            };

            return View(aCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCompany(AssignCompanyModel aCM)
        {
            if (ModelState.IsValid)
            {
                var newFirma = new Firma()
                {
                    IdFirma = aCM.Firma.IdFirma,
                    Nazwa = aCM.Firma.Nazwa
                };

                _s16693context.Add(newFirma);
                await _s16693context.SaveChangesAsync();

                var newKF = new KlientFirma()
                {
                    IdKlient = aCM.Klient.IdKlient,
                    IdFirma = newFirma.IdFirma
                };

                _s16693context.Add(newKF);

                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                return View("AssignCompany", aCM);
            }
            return View(aCM);
        }

        public async Task<IActionResult> Meetings()
        {
            var meet = await _s16693context.PracownikKlient
                .Include(k => k.IdKlientNavigation)
                    .ThenInclude(o => o.IdKlientNavigation)
                .Include(p => p.IdPracownikNavigation)
                    .ThenInclude(po => po.IdPracownikNavigation)
                .Where(x => x.IdKlientNavigation.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                .ToListAsync();
            return View(meet);
        }

        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _s16693context.Projekt
            .FirstOrDefaultAsync(e => e.IdProjekt == id);

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return View(project);
            }


        }

        public async Task<IActionResult> Team(int? id)
        {
            var team = await _s16693context.ZespolProjekt
                .Include(p => p.IdProjektNavigation)
                .Include(z => z.IdZespolNavigation)
                .FirstOrDefaultAsync(x => x.IdProjekt == id && x.DataWypisaniaZespolu == null);

            var members = await _s16693context.PracownikZespol
                .Include(z => z.IdZespolNavigation)
                .Include(p => p.IdPracownikNavigation)
                    .ThenInclude(o => o.IdPracownikNavigation)
                    .Where(x => x.IdZespol == team.IdZespol)
                    .ToListAsync();

            return View(members);

        }

        public async Task<IActionResult> Contract(int? id)
        {
            var contract = await _s16693context.ProjektPakiet
                .Include(pr => pr.IdProjektNavigation)
                .Include(pa => pa.IdPakietNavigation)
                .Where(x => x.IdProjekt == id && x.DataZakonczeniaWspolpracy == null)
                .OrderByDescending(e => e.IdPakiet)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Profile()
        {
            var kl = await _s16693context.Klient
                .Include(o => o.IdKlientNavigation)
                .FirstOrDefaultAsync(i => i.IdKlientNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);
           
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