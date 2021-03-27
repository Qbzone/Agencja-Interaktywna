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

            /*var zespol = _s16693context.Zespol.ToList();

            var pakiet = _s16693context.Pakiet.ToList();

            var firma = _s16693context.Firma.ToList();

            return View(new Tuple<Projekt, List<Zespol>, List<Pakiet>, List<Firma>>(projekt, zespol, pakiet, firma));*/

        }

        public Zespol GetZespolById(int? id)
        {
            return _s16693context.Zespol
                .FirstOrDefault(o => o.IdZespol == id);
        }
        public Pakiet GetPakietById(int? id)
        {
            return _s16693context.Pakiet
                .FirstOrDefault(o => o.IdPakiet == id);
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

                //var tmpProjekt = _s16693context.Projekt.FirstOrDefault(e => e.Nazwa == pCM.projekt.Nazwa);

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

                /*ZespolProjekt zp = new ZespolProjekt();
                var zespol = GetZespolById(idZespol);
                zp.IdProjekt = newProject.IdProjekt;
                zp.IdZespolNavigation = zespol;
                zp.DataPrzypisaniaZespolu = DateTime.Now;

                ProjektPakiet pp = new ProjektPakiet();
                var pakiet = GetPakietById(idUmowa);
                pp.IdProjekt = newProject.IdProjekt;
                pp.IdPakietNavigation = pakiet;
                pp.DataRozpoczeciaWspolpracy = DateTime.Now;

                _s16693context.Add(zp);
                _s16693context.Add(pp);*/

                _s16693context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                return View("ProjectCreate", pCM);
            }
            return View(pCM);
        }

        public IActionResult ProjectEdit()
        {
            return View();
        }

        public IActionResult ProjectDelete()
        {
            return View();
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
                .FirstOrDefault(x => x.IdProjekt == id);

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