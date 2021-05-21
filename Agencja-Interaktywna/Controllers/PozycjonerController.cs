using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Agencja_Interaktywna.Models;
using Agencja_Interaktywna.Models.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencja_Interaktywna.Controllers
{
    [Authorize(Roles = "Pozycjoner")]
    public class PozycjonerController : Controller
    {
        private readonly s16693Context _s16693context = new s16693Context();
        public IActionResult Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var zespol = _s16693context.Zespol
                    .Include(pz => pz.PracownikZespol)
                        .ThenInclude(p => p.IdPracownikNavigation)
                            .ThenInclude(o => o.IdPracownikNavigation)
                    .Where(x => x.PracownikZespol.Any(e => e.IdPracownikNavigation.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value)).ToList();

            List<Projekt> projekts = new List<Projekt>();

            foreach (var item in zespol)
            {

                projekts.AddRange(_s16693context.Projekt
                .Include(zp => zp.ZespolProjekt)
                    .ThenInclude(z => z.IdZespolNavigation)
                .Where(x => x.ZespolProjekt.Any(e => e.IdZespol == item.IdZespol && e.DataWypisaniaZespolu == null)).ToList());
            }

            return View(projekts);
        }

        [HttpGet]
        public IActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _s16693context.Projekt
            .FirstOrDefault(e => e.IdProjekt == id);

            var tasks = _s16693context.UslugaProjekt
                .Include(p => p.IdProjektNavigation)
                .Include(z => z.IdUslugaNavigation)
                .Where(e => e.IdProjekt == id && e.IdUslugaNavigation.Klasyfikacja == HttpContext.User.FindFirst(ClaimTypes.Role).Value)
                .ToList();

            var pDM = new ProjectDetailsModel
            {
                projekt = project,
                zadanies = tasks
            };

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return View(pDM);
            }
        }

        [HttpGet]
        public IActionResult Teams()
        {
            var teams = _s16693context.Zespol
                    .Include(pz => pz.PracownikZespol)
                        .ThenInclude(p => p.IdPracownikNavigation)
                            .ThenInclude(o => o.IdPracownikNavigation)
                    .Where(x => x.PracownikZespol.Any(e => e.IdPracownikNavigation.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value)).ToList();

            return View(teams);
        }

        [HttpGet]
        public IActionResult Team(int? id, string view)
        {
            if (view.Equals("Project"))
            {
                var team = _s16693context.ZespolProjekt
                    .Include(p => p.IdProjektNavigation)
                    .Include(z => z.IdZespolNavigation)
                    .FirstOrDefault(x => x.IdProjekt == id && x.DataWypisaniaZespolu == null);


                var members = _s16693context.PracownikZespol
                    .Include(z => z.IdZespolNavigation)
                    .Include(p => p.IdPracownikNavigation)
                        .ThenInclude(o => o.IdPracownikNavigation)
                        .Where(x => x.IdZespol == team.IdZespol)
                        .ToList();
                return View(members);
            }
            else if (view.Equals("Teams"))
            {
                var members = _s16693context.PracownikZespol
                    .Include(z => z.IdZespolNavigation)
                    .Include(p => p.IdPracownikNavigation)
                        .ThenInclude(o => o.IdPracownikNavigation)
                        .Where(x => x.IdZespol == id)
                        .ToList();
                return View(members);
            }

            return NotFound();

        }

        [HttpGet]
        public IActionResult TaskEdit(int? id1, int? id2, string data)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(data);
            var uslugaprojekt = _s16693context.UslugaProjekt.FirstOrDefault(x => x.IdProjekt == id1 & x.IdUsluga == id2 & x.DataPrzypisaniaZadania == fromDateAsDateTime);
            if (uslugaprojekt == null)
            {
                return NotFound();
            }

            var pakiet = _s16693context.ProjektPakiet.FirstOrDefault(x => x.IdProjekt == id1 && x.DataZakonczeniaWspolpracy == null);
            var pU = _s16693context.PakietUsluga.Where(x => x.IdPakiet == pakiet.IdPakiet).Include(u => u.IdUslugaNavigation).ToList();
            List<Usluga> uslugas = new List<Usluga>();
            foreach (var item in pU)
            {
                uslugas.Add(_s16693context.Usluga.FirstOrDefault(x => x.IdUsluga == item.IdUsluga));
            }

            var tEM = new TaskEditModel
            {
                UslugaProjekt = uslugaprojekt,
                uslugas = uslugas
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TaskEdit(TaskEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _s16693context.Update(tEM.UslugaProjekt);
                _s16693context.SaveChanges();

                return RedirectToAction("ProjectDetails", new { id = tEM.UslugaProjekt.IdProjekt });

            }
            else if (!ModelState.IsValid)
            {
                return View("TaskEdit", tEM);
            }
            return View(tEM);
        }

        public IActionResult TaskDetails(int? id1, int? id2, string data)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(data);
            var uslugaprojekt = _s16693context.UslugaProjekt
                .Include(x => x.IdUslugaNavigation)
                .FirstOrDefault(x => x.IdProjekt == id1 & x.IdUsluga == id2 & x.DataPrzypisaniaZadania == fromDateAsDateTime);

            if (uslugaprojekt == null)
            {
                return NotFound();
            }

            return View(uslugaprojekt);
        }

        public IActionResult Meetings()
        {
            var meetings = _s16693context.PracownikKlient
                .Include(k => k.IdKlientNavigation)
                    .ThenInclude(o => o.IdKlientNavigation)
                .Include(p => p.IdPracownikNavigation)
                    .ThenInclude(po => po.IdPracownikNavigation)
                .Where(e => e.IdPracownikNavigation.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value).ToList();

            return View(meetings);
        }



        public IActionResult Profile()
        {
            var kl = _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .FirstOrDefault(i => i.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            if (kl == null)
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