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
        public IActionResult ProjectCreate()
        {
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
                    _s16693context.Update(oldZP);

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
                    _s16693context.Update(oldPP);

                    var newPP = new ProjektPakiet();
                    newPP.IdProjekt = pEM.projekt.IdProjekt;
                    newPP.IdPakiet = pEM.pakiet.IdPakiet;
                    newPP.DataRozpoczeciaWspolpracy = DateTime.Now;

                    _s16693context.Add(newPP);
                    _s16693context.SaveChanges();

                }

                _s16693context.SaveChanges();
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
        public IActionResult ProjectDelete(ProjectDetailsModel pDM)
        {

            foreach (ZespolProjekt delZP in _s16693context.ZespolProjekt)
            {
                if (delZP.IdProjekt == pDM.projekt.IdProjekt)
                {
                    _s16693context.ZespolProjekt.Remove(delZP);
                }
            }

            foreach (ProjektPakiet delPP in _s16693context.ProjektPakiet)
            {
                if (delPP.IdProjekt == pDM.projekt.IdProjekt)
                {
                    _s16693context.ProjektPakiet.Remove(delPP);
                }
            }

            foreach (ZadanieProjekt delZadP in _s16693context.ZadanieProjekt)
            {
                if (delZadP.IdProjekt == pDM.projekt.IdProjekt)
                {
                    _s16693context.ZadanieProjekt.Remove(delZadP);
                }
            }

            var projekt = _s16693context.Projekt.Find(pDM.projekt.IdProjekt);

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
            var meetings = _s16693context.PracownikKlient
                .Include(k => k.IdKlientNavigation)
                    .ThenInclude(o => o.IdKlientNavigation)
                .Include(p => p.IdPracownikNavigation)
                    .ThenInclude(po => po.IdPracownikNavigation)
                .ToList();
            
            return View(meetings);
        }

        [HttpGet]
        public IActionResult MeetingsCreate()
        {
            var klient = _s16693context.Klient.Include(o => o.IdKlientNavigation).ToList();
            var pracownik = _s16693context.Pracownik.Include(o => o.IdPracownikNavigation).ToList();
            
            var mCM = new MeetingCreateModel
            {
                klients = klient,
                pracowniks = pracownik
            };

            return View(mCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MeetingsCreate(MeetingCreateModel mCM)
        {
            if (ModelState.IsValid)
            {
                PracownikKlient newSpotkanie = new PracownikKlient();
                newSpotkanie.MiejsceSpotkania = mCM.PracownikKlient.MiejsceSpotkania;
                newSpotkanie.DataRozpoczeciaSpotkania = mCM.PracownikKlient.DataRozpoczeciaSpotkania;
                newSpotkanie.DataZakonczeniaSpotkania = mCM.PracownikKlient.DataZakonczeniaSpotkania;
                newSpotkanie.IdPracownik = mCM.PracownikKlient.IdPracownik;
                newSpotkanie.IdKlient = mCM.PracownikKlient.IdKlient;
                
                _s16693context.Add(newSpotkanie);
                _s16693context.SaveChanges();
                
                return RedirectToAction(nameof(Meetings));
            }
            else if (!ModelState.IsValid)
            {
                return View("MeetingsCreate", mCM);
            }
            return View(mCM);
        }
    
        [HttpGet]
        public IActionResult MeetingsEdit(int? id1, int? id2, string data)
        {
            if (id1 == null)
            {
                return NotFound();
            }
            
            var fromDateAsDateTime = DateTime.Parse(data);
            var klient = _s16693context.Klient.Find(id1);
            var pracownik = _s16693context.Pracownik.Find(id2);
            var spotkanie =  _s16693context.PracownikKlient.FirstOrDefault(x => x.IdKlient == klient.IdKlient & x.IdPracownik == pracownik.IdPracownik & x.DataRozpoczeciaSpotkania == fromDateAsDateTime);
            if (spotkanie == null)
            {
                return NotFound();
            }

            var klients = _s16693context.Klient.Include(o => o.IdKlientNavigation).ToList();
            var pracowniks = _s16693context.Pracownik.Include(o => o.IdPracownikNavigation).ToList();

            var mEM = new MeetingEditModel
            {
                PracownikKlient = spotkanie,
                IdPracownik = spotkanie.IdPracownik,
                IdKlient = spotkanie.IdKlient,
                DataRozpoczeciaSpotkania = spotkanie.DataRozpoczeciaSpotkania,
                klients = klients,
                pracowniks = pracowniks
            };

            return View(mEM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MeetingsEdit(MeetingEditModel mEM)
        {
            if (ModelState.IsValid)
            {
                var oldPK = _s16693context.PracownikKlient
                    .Where(x => x.IdPracownik == mEM.IdPracownik && x.IdKlient == mEM.IdKlient && x.DataRozpoczeciaSpotkania == mEM.DataRozpoczeciaSpotkania)
                    .FirstOrDefault();

                if(mEM.PracownikKlient.IdPracownik != oldPK.IdPracownik || mEM.PracownikKlient.IdKlient != oldPK.IdKlient || mEM.PracownikKlient.DataRozpoczeciaSpotkania != oldPK.DataRozpoczeciaSpotkania)
                {
                    _s16693context.Remove(oldPK);

                    PracownikKlient newPK = new PracownikKlient();
                    newPK.MiejsceSpotkania = mEM.PracownikKlient.MiejsceSpotkania;
                    newPK.DataRozpoczeciaSpotkania = mEM.PracownikKlient.DataRozpoczeciaSpotkania;
                    newPK.DataZakonczeniaSpotkania = mEM.PracownikKlient.DataZakonczeniaSpotkania;
                    newPK.IdPracownik = mEM.PracownikKlient.IdPracownik;
                    newPK.IdKlient = mEM.PracownikKlient.IdKlient;

                    _s16693context.Add(newPK);
                    _s16693context.SaveChanges();

                    return RedirectToAction(nameof(Meetings));
                }
                else
                {
                    _s16693context.Entry(oldPK).State = EntityState.Detached;
                    _s16693context.Update(mEM.PracownikKlient);
                    _s16693context.SaveChanges();

                    return RedirectToAction(nameof(Meetings));
                }

            }
            else if (!ModelState.IsValid)
            {
                return View("MeetingsEdit", mEM);
            }
            return View(mEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MeetingsDelete(int? id1, int? id2, string data)
        {
            var fromDateAsDateTime = DateTime.Parse(data);
            var spotkanie = _s16693context.PracownikKlient.FirstOrDefault(x => x.IdKlient == id1 & x.IdPracownik == id2 & x.DataRozpoczeciaSpotkania == fromDateAsDateTime);

            _s16693context.Remove(spotkanie);
            _s16693context.SaveChanges();

            return RedirectToAction(nameof(Meetings));
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
            var teams = _s16693context.Zespol.ToList();

            return View(teams);
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

        [HttpGet]
        public IActionResult TeamsCreate()
        {
            var pracownik = _s16693context.Pracownik.Include(o => o.IdPracownikNavigation).ToList();

            var tCM = new TeamCreateModel
            {
                pracowniks = pracownik
            };

            return View(tCM);
        }

        public IActionResult TeamsEdit()
        {
            return View();
        }

        public IActionResult TeamsDelete()
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