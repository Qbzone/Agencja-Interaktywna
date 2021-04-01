using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Agencja_Interaktywna.Models;
using Agencja_Interaktywna.Models.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencja_Interaktywna.Controllers
{
    [Authorize(Roles = "Szef")]
    public class SzefController : Controller
    {
        private readonly s16693Context _s16693context = new s16693Context();
        public IActionResult Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var pr = _s16693context.Projekt.ToList();

            return View(pr);
        }

        public IActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _s16693context.Projekt
            .FirstOrDefault(e => e.IdProjekt == id);

            var tasks = _s16693context.ZadanieProjekt
                .Include(p => p.IdProjektNavigation)
                .Include(z => z.IdZadanieNavigation)
                .Where(e => e.IdProjekt == id)
                .ToList();

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return View(new Tuple<Projekt, List<ZadanieProjekt>>(project, tasks));
            }
        }

        [HttpGet]
        public IActionResult ProjectCreate()
        {
            var projekt = new Projekt();
            var firma = from e in _s16693context.Firma select e;
            var zespol = from e in _s16693context.Zespol select e;
            var pakiet = from e in _s16693context.Pakiet select e;

            var pCM = new ProjectCreateModel
            {
                firmas = firma.ToList(),
                zespols = zespol.ToList(),
                pakiets = pakiet.ToList()
            };

            return View(pCM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProjectCreate(ProjectCreateModel pCM)
        {
            if (ModelState.IsValid)
            {
                Projekt newProjekt = new Projekt();
                newProjekt.Nazwa = pCM.projekt.Nazwa;
                newProjekt.Logo = pCM.projekt.Logo;
                newProjekt.IdFirma = pCM.projekt.IdFirma;
                _s16693context.Add(newProjekt);
                _s16693context.SaveChanges();

                ZespolProjekt newZP = new ZespolProjekt();
                newZP.IdProjekt = newProjekt.IdProjekt;
                newZP.IdZespol = pCM.zespol.IdZespol;
                newZP.DataPrzypisaniaZespolu = DateTime.Now;

                ProjektPakiet newPP = new ProjektPakiet();
                newPP.IdProjekt = newProjekt.IdProjekt;
                newPP.IdPakiet = pCM.pakiet.IdPakiet;
                newPP.DataRozpoczeciaWspolpracy = DateTime.Now;

                _s16693context.Add(newZP);
                _s16693context.Add(newPP);

                _s16693context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                return View("ProjectCreate", pCM);
            }
            return View(pCM);
        }

        [HttpGet]
        public IActionResult ProjectEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var projekt = _s16693context.Projekt.Find(id);

            if (projekt == null)
            {
                return NotFound();
            }

            var zp = _s16693context.ZespolProjekt.FirstOrDefault(x => x.IdProjekt == projekt.IdProjekt && x.DataWypisaniaZespolu == null);
            var pp = _s16693context.ProjektPakiet.FirstOrDefault(x => x.IdProjekt == projekt.IdProjekt && x.DataZakonczeniaWspolpracy == null);
            var zespol = _s16693context.Zespol.FirstOrDefault(x => x.IdZespol == zp.IdZespol);
            var pakiet = _s16693context.Pakiet.FirstOrDefault(x => x.IdPakiet == pp.IdPakiet);
            var IdZespol = zespol.IdZespol;
            var IdPakiet = pakiet.IdPakiet;
            var firmy = from e in _s16693context.Firma select e;
            var zespoly = from e in _s16693context.Zespol select e;
            var pakiety = from e in _s16693context.Pakiet select e;

            var pEM = new ProjectEditModel
            {
                projekt = projekt,
                zespol = zespol,
                pakiet = pakiet,
                firmas = firmy.ToList(),
                zespols = zespoly.ToList(),
                pakiets = pakiety.ToList(),
                IdZespol = IdZespol,
                IdPakiet = IdPakiet
            };

            return View(pEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProjectEdit(ProjectEditModel pEM)
        {
            if (ModelState.IsValid)
            {
                _s16693context.Update(pEM.projekt);

                var oldZP = _s16693context.ZespolProjekt
                    .Where(x => x.IdZespol == pEM.IdZespol && x.IdProjekt == pEM.projekt.IdProjekt && x.DataWypisaniaZespolu == null)
                    .FirstOrDefault();
                var oldPP = _s16693context.ProjektPakiet
                    .Where(x => x.IdPakiet == pEM.IdPakiet && x.IdProjekt == pEM.projekt.IdProjekt && x.DataZakonczeniaWspolpracy == null)
                    .FirstOrDefault();

                if (oldZP.IdZespol != pEM.zespol.IdZespol)
                {

                    oldZP.DataWypisaniaZespolu = DateTime.Now;

                    var newZP = new ZespolProjekt();
                    newZP.IdProjekt = pEM.projekt.IdProjekt;
                    newZP.IdZespol = pEM.zespol.IdZespol;
                    newZP.DataPrzypisaniaZespolu = DateTime.Now;

                    _s16693context.Add(newZP);
                    _s16693context.SaveChanges();

                }

                if (oldPP.IdPakiet != pEM.pakiet.IdPakiet)
                {

                    oldPP.DataZakonczeniaWspolpracy = DateTime.Now;

                    var newPP = new ProjektPakiet();
                    newPP.IdProjekt = pEM.projekt.IdProjekt;
                    newPP.IdPakiet = pEM.pakiet.IdPakiet;
                    newPP.DataRozpoczeciaWspolpracy = DateTime.Now;

                    _s16693context.Add(newPP);
                    _s16693context.SaveChanges();

                }

                return RedirectToAction(nameof(Index));

            }
            else if (!ModelState.IsValid)
            {
                return View("ProjectEdit", pEM);
            }
            return View(pEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProjectDelete(int id)
        {
            foreach (ZespolProjekt delZP in _s16693context.ZespolProjekt)
            {
                if (delZP.IdProjekt == id)
                {
                    _s16693context.ZespolProjekt.Remove(delZP);
                }
            }

            foreach (ProjektPakiet delPP in _s16693context.ProjektPakiet)
            {
                if (delPP.IdProjekt == id)
                {
                    _s16693context.ProjektPakiet.Remove(delPP);
                }
            }

            foreach (ZadanieProjekt delZadP in _s16693context.ZadanieProjekt)
            {
                if (delZadP.IdProjekt == id)
                {
                    _s16693context.ZadanieProjekt.Remove(delZadP);
                }
            }

            var projekt = _s16693context.Projekt.Find(id);

            _s16693context.Remove(projekt);
            _s16693context.SaveChanges();

            return RedirectToAction(nameof(Index));
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

        public IActionResult Meetings()
        {
            return View();
        }

        public IActionResult MeetingsCreate()
        {
            return View();
        }

        public IActionResult MeetingsEdit()
        {
            return View();
        }

        public IActionResult MeetingsDelete()
        {
            return View();
        }

        public IActionResult Profile()
        {
            var sz = _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .FirstOrDefault(i => i.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            if (sz == null)
            {
                return NotFound();
            }
            else
            {
                return View(sz);
            }
        }

        public IActionResult Teams()
        {
            return View();
        }

        public IActionResult Team(int? id)
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

        public IActionResult TeamCreate()
        {
            return View();
        }

        public IActionResult TeamEdit()
        {
            return View();
        }

        public IActionResult TeamDelete()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult TaskDetails()
        {
            return View();
        }

        public IActionResult TaskCreate()
        {
            return View();
        }

        public IActionResult TaskEdit()
        {
            return View();
        }

        public IActionResult TaskDelete()
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