using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Agencja_Interaktywna.Models;
using Agencja_Interaktywna.Models.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                var newProjekt = new Projekt()
                {
                    Nazwa = pCM.projekt.Nazwa,
                    Logo = pCM.projekt.Logo,
                    IdFirma = pCM.projekt.IdFirma
                };

                _s16693context.Add(newProjekt);
                _s16693context.SaveChanges();

                var newZP = new ZespolProjekt()
                {
                    IdProjekt = newProjekt.IdProjekt,
                    IdZespol = pCM.zespol.IdZespol,
                    DataPrzypisaniaZespolu = DateTime.Now
                };

                var newPP = new ProjektPakiet()
                {
                    IdProjekt = newProjekt.IdProjekt,
                    IdPakiet = pCM.pakiet.IdPakiet,
                    DataRozpoczeciaWspolpracy = DateTime.Now
                };

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

                    var newZP = new ZespolProjekt()
                    {
                        IdProjekt = pEM.projekt.IdProjekt,
                        IdZespol = pEM.zespol.IdZespol,
                        DataPrzypisaniaZespolu = DateTime.Now
                    };

                    _s16693context.Add(newZP);
                    _s16693context.SaveChanges();

                }

                if (oldPP.IdPakiet != pEM.pakiet.IdPakiet)
                {

                    oldPP.DataZakonczeniaWspolpracy = DateTime.Now;
                    _s16693context.Update(oldPP);

                    var newPP = new ProjektPakiet()
                    {
                        IdProjekt = pEM.projekt.IdProjekt,
                        IdPakiet = pEM.pakiet.IdPakiet,
                        DataRozpoczeciaWspolpracy = DateTime.Now
                    };

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

            foreach (UslugaProjekt delZadP in _s16693context.UslugaProjekt)
            {
                if (delZadP.IdProjekt == pDM.projekt.IdProjekt)
                {
                    _s16693context.UslugaProjekt.Remove(delZadP);
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
                var newSpotkanie = new PracownikKlient()
                {
                    MiejsceSpotkania = mCM.PracownikKlient.MiejsceSpotkania,
                    DataRozpoczeciaSpotkania = mCM.PracownikKlient.DataRozpoczeciaSpotkania,
                    DataZakonczeniaSpotkania = mCM.PracownikKlient.DataZakonczeniaSpotkania,
                    IdPracownik = mCM.PracownikKlient.IdPracownik,
                    IdKlient = mCM.PracownikKlient.IdKlient
                };

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
            var spotkanie = _s16693context.PracownikKlient.FirstOrDefault(x => x.IdKlient == id1 & x.IdPracownik == id2 & x.DataRozpoczeciaSpotkania == fromDateAsDateTime);
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

                if (mEM.PracownikKlient.IdPracownik != oldPK.IdPracownik || mEM.PracownikKlient.IdKlient != oldPK.IdKlient || mEM.PracownikKlient.DataRozpoczeciaSpotkania != oldPK.DataRozpoczeciaSpotkania)
                {
                    _s16693context.Remove(oldPK);

                    var newPK = new PracownikKlient()
                    {
                        MiejsceSpotkania = mEM.PracownikKlient.MiejsceSpotkania,
                        DataRozpoczeciaSpotkania = mEM.PracownikKlient.DataRozpoczeciaSpotkania,
                        DataZakonczeniaSpotkania = mEM.PracownikKlient.DataZakonczeniaSpotkania,
                        IdPracownik = mEM.PracownikKlient.IdPracownik,
                        IdKlient = mEM.PracownikKlient.IdKlient
                    };

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

        [HttpGet]
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

        [HttpGet]
        public IActionResult Teams()
        {
            var teams = _s16693context.Zespol.ToList();

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
        public IActionResult TeamsCreate()
        {
            var pracownik = _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .Select(x => new SelectListItem()
                {
                    Text = x.IdPracownikNavigation.AdresEmail,
                    Value = x.IdPracownik.ToString()
                }).ToList();

            var tCM = new TeamCreateModel
            {
                pracowniks = pracownik
            };

            return View(tCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeamsCreate(TeamCreateModel tCM)
        {

            if (ModelState.IsValid)
            {
                var newTeam = new Zespol()
                {
                    Nazwa = tCM.zespol.Nazwa
                };

                _s16693context.Add(newTeam);
                _s16693context.SaveChanges();

                var pracownikIds = tCM.pracowniks.Where(x => x.Selected).Select(y => y.Value);

                foreach (var id in pracownikIds)
                {
                    var PZ = new PracownikZespol()
                    {
                        IdPracownik = int.Parse(id),
                        IdZespol = newTeam.IdZespol,
                        DataPrzypisaniaPracownika = DateTime.Now
                    };
                    _s16693context.Add(PZ);
                }

                _s16693context.SaveChanges();
                return RedirectToAction(nameof(Teams));
            }
            else if (!ModelState.IsValid)
            {
                return View("TeamsCreate", tCM);
            }
            return View(tCM);
        }

        [HttpGet]
        public IActionResult TeamsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var zespol = _s16693context.Zespol
                .Include(z => z.PracownikZespol)
                .ThenInclude(p => p.IdPracownikNavigation).AsNoTracking()
                .SingleOrDefault(z => z.IdZespol == id);

            if (zespol == null)
            {
                return NotFound();
            }

            var allpracownik = _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .Select(x => new CheckBoxItem()
                {
                    Id = x.IdPracownik,
                    Nazwa = x.IdPracownikNavigation.AdresEmail,
                    IsChecked = x.PracownikZespol.Any(x => x.IdZespol == zespol.IdZespol) ? true : false
                }).ToList();

            var tEM = new TeamEditModel()
            {
                zespol = zespol,
                pracowniks = allpracownik,
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeamsEdit(TeamEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _s16693context.Update(tEM.zespol);
                _s16693context.SaveChanges();
                List<PracownikZespol> pracowniklist = new List<PracownikZespol>();
                var length = 0;
                var errors = 0;

                foreach (var item in tEM.pracowniks)
                {
                    length++;
                    if (item.IsChecked == true)
                    {
                        var PZ = new PracownikZespol()
                        {
                            IdPracownik = item.Id,
                            IdZespol = tEM.zespol.IdZespol,
                            DataPrzypisaniaPracownika = DateTime.Now
                        };
                        _s16693context.Add(PZ);
                    }
                    else if (item.IsChecked == false)
                    {
                        errors++;
                    }
                }
                if (errors == length)
                {
                    var allpracownik = _s16693context.Pracownik
                        .Include(o => o.IdPracownikNavigation)
                        .Select(x => new CheckBoxItem()
                        {
                            Id = x.IdPracownik,
                            Nazwa = x.IdPracownikNavigation.AdresEmail,
                            IsChecked = x.PracownikZespol.Any(x => x.IdZespol == tEM.zespol.IdZespol) ? true : false
                        }).ToList();

                    tEM.pracowniks = allpracownik;

                    ViewBag.Error = "Musi byc wybrany przynajmniej jeden pracownik";
                    return View("TeamsEdit", tEM);
                }

                var dt = _s16693context.PracownikZespol.Where(x => x.IdZespol == tEM.zespol.IdZespol).ToList();
                foreach (var item in dt)
                {
                    _s16693context.PracownikZespol.Remove(item);
                    _s16693context.SaveChanges();
                }

                var idS = _s16693context.PracownikZespol.Where(x => x.IdZespol == tEM.zespol.IdZespol).ToList();
                foreach (var item in pracowniklist)
                {
                    if (idS.Contains(item))
                    {
                        _s16693context.PracownikZespol.Add(item);
                        _s16693context.SaveChanges();
                    }
                }
                return RedirectToAction(nameof(Teams));
            }

            else if (!ModelState.IsValid)
            {
                return View("TeamsEdit", tEM);
            }
            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeamsDelete(int? id)
        {
            foreach (var delPZ in _s16693context.PracownikZespol)
            {
                if (delPZ.IdZespol == id)
                {
                    _s16693context.PracownikZespol.Remove(delPZ);
                }
            }

            var zespol = _s16693context.Zespol.Find(id);

            _s16693context.Remove(zespol);
            _s16693context.SaveChanges();

            return RedirectToAction(nameof(Teams));
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
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

        [HttpGet]
        public IActionResult TaskCreate(int? id)
        {
            var pakiet = _s16693context.ProjektPakiet.FirstOrDefault(x => x.IdProjekt == id && x.DataZakonczeniaWspolpracy == null);
            var pU = _s16693context.PakietUsluga.Where(x => x.IdPakiet == pakiet.IdPakiet).Include(u => u.IdUslugaNavigation).ToList();
            List<Usluga> uslugas = new List<Usluga>();
            foreach (var item in pU)
            {
                uslugas.Add(_s16693context.Usluga.FirstOrDefault(x => x.IdUsluga == item.IdUsluga));
            }

            var projekt = _s16693context.Projekt.Find(id);

            var tCM = new TaskCreateModel
            {
                uslugas = uslugas,
                projekt = projekt
            };

            return View(tCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TaskCreate(TaskCreateModel tCM)
        {
            if (ModelState.IsValid)
            {
                var newZadanie = new UslugaProjekt()
                {
                    IdProjekt = tCM.projekt.IdProjekt,
                    IdUsluga = tCM.UslugaProjekt.IdUsluga,
                    Opis = tCM.UslugaProjekt.Opis,
                    DataPrzypisaniaZadania = tCM.UslugaProjekt.DataPrzypisaniaZadania,
                    DataZakonczeniaZadania = tCM.UslugaProjekt.DataZakonczeniaZadania,
                    Status = tCM.UslugaProjekt.Status
                };

                _s16693context.Add(newZadanie);
                _s16693context.SaveChanges();

                return RedirectToAction("ProjectDetails", new { id = tCM.projekt.IdProjekt });
            }
            else if (!ModelState.IsValid)
            {
                return View("MeetingsCreate", tCM);
            }
            return View(tCM);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TaskDelete(int? id1, int? id2, string data)
        {
            var fromDateAsDateTime = DateTime.Parse(data);
            var zadanie = _s16693context.UslugaProjekt.FirstOrDefault(x => x.IdProjekt == id1 & x.IdUsluga == id2 & x.DataPrzypisaniaZadania == fromDateAsDateTime);

            _s16693context.Remove(zadanie);
            _s16693context.SaveChanges();

            return RedirectToAction("ProjectDetails", new { id = id1 });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}