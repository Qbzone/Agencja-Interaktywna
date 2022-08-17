using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Agencja_Interaktywna.Models;
using Agencja_Interaktywna.Models.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agencja_Interaktywna.Controllers
{
    [Authorize(Roles = "Szef")]
    public class BossController : Controller
    {
        private readonly Models.InteractiveAgencyContext _s16693context = new Models.InteractiveAgencyContext();
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public BossController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var pr = await _s16693context.Projekt.ToListAsync();

            return View(pr);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _s16693context.Projekt
            .FirstOrDefaultAsync(e => e.IdProjekt == id);

            var tasks = await _s16693context.UslugaProjekt
                .Include(p => p.IdProjektNavigation)
                .Include(z => z.IdUslugaNavigation)
                .Where(e => e.IdProjekt == id)
                .ToListAsync();

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
        public async Task<IActionResult> ProjectCreate()
        {
            var firma = from e in _s16693context.Firma select e;
            var zespol = from e in _s16693context.Zespol select e;
            var pakiet = from e in _s16693context.Pakiet select e;

            var pCM = new ProjectCreateModel
            {
                firmas = await firma.ToListAsync(),
                zespols = await zespol.ToListAsync(),
                pakiets = await pakiet.ToListAsync()
            };

            return View(pCM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> ProjectCreate(ProjectCreateModel pCM)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.Combine(hostingEnvironment.WebRootPath + "/images", Path.GetFileName(pCM.FormFile.FileName));
                pCM.FormFile.CopyTo(new FileStream(fileName, FileMode.Create));

                var newProjekt = new Project()
                {
                    Nazwa = pCM.projekt.Nazwa,
                    Logo = "images/" + Path.GetFileName(pCM.FormFile.FileName),
                    IdFirma = pCM.projekt.IdFirma
                };

                _s16693context.Add(newProjekt);
                await _s16693context.SaveChangesAsync();

                var newZP = new TeamProject()
                {
                    IdProjekt = newProjekt.IdProjekt,
                    IdZespol = (int)pCM.zespol.IdZespol,
                    DataPrzypisaniaZespolu = DateTime.Now
                };

                var newPP = new ProjectPackage()
                {
                    IdProjekt = newProjekt.IdProjekt,
                    IdPakiet = (int)pCM.pakiet.IdPakiet,
                    DataRozpoczeciaWspolpracy = DateTime.Now
                };

                _s16693context.Add(newZP);
                _s16693context.Add(newPP);

                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                var firma = from e in _s16693context.Firma select e;
                var zespol = from e in _s16693context.Zespol select e;
                var pakiet = from e in _s16693context.Pakiet select e;

                pCM.zespols = await zespol.ToListAsync();
                pCM.pakiets = await pakiet.ToListAsync();
                pCM.firmas = await firma.ToListAsync();

                return View("ProjectCreate", pCM);
            }
            return View(pCM);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekt = await _s16693context.Projekt.FindAsync(id);

            if (projekt == null)
            {
                return NotFound();
            }

            var zp = await _s16693context.ZespolProjekt.FirstOrDefaultAsync(x => x.IdProjekt == projekt.IdProjekt && x.DataWypisaniaZespolu == null);
            var pp = await _s16693context.ProjektPakiet.FirstOrDefaultAsync(x => x.IdProjekt == projekt.IdProjekt && x.DataZakonczeniaWspolpracy == null);
            var zespol = await _s16693context.Zespol.FirstOrDefaultAsync(x => x.IdZespol == zp.IdZespol);
            var pakiet = await _s16693context.Pakiet.FirstOrDefaultAsync(x => x.IdPakiet == pp.IdPakiet);
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
                firmas = await firmy.ToListAsync(),
                zespols = await zespoly.ToListAsync(),
                pakiets = await pakiety.ToListAsync(),
                IdZespol = (int)IdZespol,
                IdPakiet = (int)IdPakiet
            };

            return View(pEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> ProjectEdit(ProjectEditModel pEM)
        {
            if (ModelState.IsValid)
            {

                _s16693context.Update(pEM.projekt);

                var oldZP = await _s16693context.ZespolProjekt
                    .Where(x => x.IdZespol == pEM.IdZespol && x.IdProjekt == pEM.projekt.IdProjekt && x.DataWypisaniaZespolu == null)
                    .FirstOrDefaultAsync();
                var oldPP = await _s16693context.ProjektPakiet
                    .Where(x => x.IdPakiet == pEM.IdPakiet && x.IdProjekt == pEM.projekt.IdProjekt && x.DataZakonczeniaWspolpracy == null)
                    .FirstOrDefaultAsync();

                if (oldZP.IdZespol != pEM.zespol.IdZespol)
                {

                    oldZP.DataWypisaniaZespolu = DateTime.Now;
                    _s16693context.Update(oldZP);

                    var newZP = new TeamProject()
                    {
                        IdProjekt = pEM.projekt.IdProjekt,
                        IdZespol = (int)pEM.zespol.IdZespol,
                        DataPrzypisaniaZespolu = DateTime.Now
                    };

                    _s16693context.Add(newZP);
                    await _s16693context.SaveChangesAsync();

                }

                if (oldPP.IdPakiet != pEM.pakiet.IdPakiet)
                {

                    oldPP.DataZakonczeniaWspolpracy = DateTime.Now;
                    _s16693context.Update(oldPP);

                    var newPP = new ProjectPackage()
                    {
                        IdProjekt = pEM.projekt.IdProjekt,
                        IdPakiet = (int)pEM.pakiet.IdPakiet,
                        DataRozpoczeciaWspolpracy = DateTime.Now
                    };

                    _s16693context.Add(newPP);
                    await _s16693context.SaveChangesAsync();

                }

                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else if (!ModelState.IsValid)
            {
                var firmy = from e in _s16693context.Firma select e;
                var zespoly = from e in _s16693context.Zespol select e;
                var pakiety = from e in _s16693context.Pakiet select e;

                var newPEM = new ProjectEditModel
                {
                    projekt = pEM.projekt,
                    zespol = pEM.zespol,
                    pakiet = pEM.pakiet,
                    firmas = await firmy.ToListAsync(),
                    zespols = await zespoly.ToListAsync(),
                    pakiets = await pakiety.ToListAsync(),
                    IdZespol = (int)pEM.IdZespol,
                    IdPakiet = (int)pEM.IdPakiet
                };
                
                return View("ProjectEdit", newPEM);
            }
            return View(pEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectDelete(ProjectDetailsModel pDM)
        {

            foreach (TeamProject delZP in _s16693context.ZespolProjekt)
            {
                if (delZP.IdProjekt == pDM.projekt.IdProjekt)
                {
                    _s16693context.ZespolProjekt.Remove(delZP);
                }
            }

            foreach (ProjectPackage delPP in _s16693context.ProjektPakiet)
            {
                if (delPP.IdProjekt == pDM.projekt.IdProjekt)
                {
                    _s16693context.ProjektPakiet.Remove(delPP);
                }
            }

            foreach (ServiceProject delZadP in _s16693context.UslugaProjekt)
            {
                if (delZadP.IdProjekt == pDM.projekt.IdProjekt)
                {
                    _s16693context.UslugaProjekt.Remove(delZadP);
                }
            }

            var projekt = await _s16693context.Projekt.FindAsync(pDM.projekt.IdProjekt);

            _s16693context.Remove(projekt);
            await _s16693context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Contract(int? id)
        {
            var contract = await _s16693context.ProjektPakiet
                .Include(pr => pr.IdProjektNavigation)
                .Include(pa => pa.IdPakietNavigation)
                .Where(x => x.IdProjekt == id)
                .OrderByDescending(e => e.IdPakiet)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Meetings()
        {
            var meetings = await _s16693context.PracownikKlient
                .Include(k => k.IdKlientNavigation)
                    .ThenInclude(o => o.IdKlientNavigation)
                .Include(p => p.IdPracownikNavigation)
                    .ThenInclude(po => po.IdPracownikNavigation)
                .ToListAsync();

            return View(meetings);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingsCreate()
        {
            var klient = await _s16693context.Klient.Include(o => o.IdKlientNavigation).ToListAsync();
            var pracownik = await _s16693context.Pracownik.Include(o => o.IdPracownikNavigation).ToListAsync();

            var mCM = new MeetingCreateModel
            {
                Klients = klient,
                Pracowniks = pracownik
            };

            return View(mCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsCreate(MeetingCreateModel mCM)
        {
            if (ModelState.IsValid)
            {
                var newSpotkanie = new EmployeeClient()
                {
                    MiejsceSpotkania = mCM.PracownikKlient.MiejsceSpotkania,
                    DataRozpoczeciaSpotkania = mCM.PracownikKlient.DataRozpoczeciaSpotkania,
                    DataZakonczeniaSpotkania = mCM.PracownikKlient.DataZakonczeniaSpotkania,
                    IdPracownik = mCM.PracownikKlient.IdPracownik,
                    IdKlient = mCM.PracownikKlient.IdKlient
                };

                _s16693context.Add(newSpotkanie);
                await _s16693context.SaveChangesAsync();

                return RedirectToAction(nameof(Meetings));
            }
            else if (!ModelState.IsValid)
            {
                var klient = await _s16693context.Klient.Include(o => o.IdKlientNavigation).ToListAsync();
                var pracownik = await _s16693context.Pracownik.Include(o => o.IdPracownikNavigation).ToListAsync();

                var newMCM = new MeetingCreateModel
                {
                    Klients = klient,
                    Pracowniks = pracownik
                };
                
                return View("MeetingsCreate", newMCM);
            }
            return View(mCM);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingsEdit(int? id1, int? id2, string data)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(data);
            var spotkanie = await _s16693context.PracownikKlient.FirstOrDefaultAsync(x => x.IdKlient == id1 & x.IdPracownik == id2 & x.DataRozpoczeciaSpotkania == fromDateAsDateTime);
            if (spotkanie == null)
            {
                return NotFound();
            }

            var klients = await _s16693context.Klient.Include(o => o.IdKlientNavigation).ToListAsync();
            var pracowniks = await _s16693context.Pracownik.Include(o => o.IdPracownikNavigation).ToListAsync();

            var mEM = new MeetingEditModel
            {
                PracownikKlient = spotkanie,
                IdPracownik = (int)spotkanie.IdPracownik,
                IdKlient = (int)spotkanie.IdKlient,
                DataRozpoczeciaSpotkania = (DateTime)spotkanie.DataRozpoczeciaSpotkania,
                klients = klients,
                pracowniks = pracowniks
            };

            return View(mEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsEdit(MeetingEditModel mEM)
        {
            if (ModelState.IsValid)
            {
                var oldPK = await _s16693context.PracownikKlient
                    .Where(x => x.IdPracownik == mEM.IdPracownik && x.IdKlient == mEM.IdKlient && x.DataRozpoczeciaSpotkania == mEM.DataRozpoczeciaSpotkania)
                    .FirstOrDefaultAsync();

                if (mEM.PracownikKlient.IdPracownik != oldPK.IdPracownik || mEM.PracownikKlient.IdKlient != oldPK.IdKlient || mEM.PracownikKlient.DataRozpoczeciaSpotkania != oldPK.DataRozpoczeciaSpotkania)
                {
                    _s16693context.Remove(oldPK);

                    var newPK = new EmployeeClient()
                    {
                        MiejsceSpotkania = mEM.PracownikKlient.MiejsceSpotkania,
                        DataRozpoczeciaSpotkania = mEM.PracownikKlient.DataRozpoczeciaSpotkania,
                        DataZakonczeniaSpotkania = mEM.PracownikKlient.DataZakonczeniaSpotkania,
                        IdPracownik = mEM.PracownikKlient.IdPracownik,
                        IdKlient = mEM.PracownikKlient.IdKlient
                    };

                    _s16693context.Add(newPK);
                    await _s16693context.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }
                else
                {
                    _s16693context.Entry(oldPK).State = EntityState.Detached;
                    _s16693context.Update(mEM.PracownikKlient);
                    await _s16693context.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }

            }
            else if (!ModelState.IsValid)
            {

                var klients = await _s16693context.Klient.Include(o => o.IdKlientNavigation).ToListAsync();
                var pracowniks = await _s16693context.Pracownik.Include(o => o.IdPracownikNavigation).ToListAsync();

                var newMEM = new MeetingEditModel
                {
                    PracownikKlient = mEM.PracownikKlient,
                    IdPracownik = (int)mEM.IdPracownik,
                    IdKlient = (int)mEM.IdKlient,
                    DataRozpoczeciaSpotkania = (DateTime)mEM.DataRozpoczeciaSpotkania,
                    klients = klients,
                    pracowniks = pracowniks
                };
                
                return View("MeetingsEdit", newMEM);
            }
            return View(mEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsDelete(int? id1, int? id2, string data)
        {
            var fromDateAsDateTime = DateTime.Parse(data);
            var spotkanie = await _s16693context.PracownikKlient.FirstOrDefaultAsync(x => x.IdKlient == id1 & x.IdPracownik == id2 & x.DataRozpoczeciaSpotkania == fromDateAsDateTime);

            _s16693context.Remove(spotkanie);
            await _s16693context.SaveChangesAsync();

            return RedirectToAction(nameof(Meetings));
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var sz = await _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .FirstOrDefaultAsync(i => i.IdPracownikNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

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
        public async Task<IActionResult> Teams()
        {
            var teams = await _s16693context.Zespol.ToListAsync();

            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> Team(int? id, string view)
        {
            if (view.Equals("Project"))
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
            else if (view.Equals("Teams"))
            {
                var members = await _s16693context.PracownikZespol
                    .Include(z => z.IdZespolNavigation)
                    .Include(p => p.IdPracownikNavigation)
                        .ThenInclude(o => o.IdPracownikNavigation)
                        .Where(x => x.IdZespol == id)
                        .ToListAsync();
                return View(members);
            }

            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult> TeamsCreate()
        {
            var pracownik = await _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .Select(x => new SelectListItem()
                {
                    Text = x.IdPracownikNavigation.AdresEmail,
                    Value = x.IdPracownik.ToString()
                }).ToListAsync();

            var tCM = new TeamCreateModel
            {
                Pracowniks = pracownik
            };

            return View(tCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsCreate(TeamCreateModel tCM)
        {

            if (ModelState.IsValid)
            {
                var newTeam = new Team()
                {
                    Nazwa = tCM.Zespol.Nazwa
                };

                var pracownikIds = tCM.Pracowniks.Where(x => x.Selected).Select(y => y.Value);

                if (pracownikIds.Count() == 0)
                {
                    var pracownik = await _s16693context.Pracownik
                        .Include(o => o.IdPracownikNavigation)
                        .Select(x => new SelectListItem()
                        {
                            Text = x.IdPracownikNavigation.AdresEmail,
                            Value = x.IdPracownik.ToString()
                        }).ToListAsync();

                    tCM.Pracowniks = pracownik;

                    return View("TeamsCreate", tCM);
                }

                _s16693context.Add(newTeam);
                await _s16693context.SaveChangesAsync();

                foreach (var id in pracownikIds)
                {
                    var PZ = new EmployeeTeam()
                    {
                        IdPracownik = int.Parse(id),
                        IdZespol = (int)newTeam.IdZespol,
                        DataPrzypisaniaPracownika = DateTime.Now
                    };
                    _s16693context.Add(PZ);
                }

                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Teams));
            }
            else if (!ModelState.IsValid)
            {
                var pracownik = await _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .Select(x => new SelectListItem()
                {
                    Text = x.IdPracownikNavigation.AdresEmail,
                    Value = x.IdPracownik.ToString()
                }).ToListAsync();

                var newTCM = new TeamCreateModel
                {
                    Pracowniks = pracownik
                };

                return View("TeamsCreate", newTCM);
            }
            return View(tCM);
        }

        [HttpGet]
        public async Task<IActionResult> TeamsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var zespol = await _s16693context.Zespol
                .Include(z => z.PracownikZespol)
                .ThenInclude(p => p.IdPracownikNavigation).AsNoTracking()
                .SingleOrDefaultAsync(z => z.IdZespol == id);

            if (zespol == null)
            {
                return NotFound();
            }

            var allpracownik = await _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .Select(x => new CheckBoxItem()
                {
                    Id = x.IdPracownik,
                    Nazwa = x.IdPracownikNavigation.AdresEmail,
                    IsChecked = x.PracownikZespol.Any(x => x.IdZespol == zespol.IdZespol) ? true : false
                }).ToListAsync();

            var tEM = new TeamEditModel()
            {
                Zespol = zespol,
                Pracowniks = allpracownik,
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsEdit(TeamEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _s16693context.Update(tEM.Zespol);
                List<EmployeeTeam> pracowniklist = new List<EmployeeTeam>();

                foreach (var item in tEM.Pracowniks)
                {
                    if (item.IsChecked == true)
                    {
                        var PZ = new EmployeeTeam()
                        {
                            IdPracownik = item.Id,
                            IdZespol = (int)tEM.Zespol.IdZespol,
                            DataPrzypisaniaPracownika = DateTime.Now
                        };
                        _s16693context.Add(PZ);
                    }
                }

                var dt = await _s16693context.PracownikZespol.Where(x => x.IdZespol == tEM.Zespol.IdZespol).ToListAsync();
                foreach (var item in dt)
                {
                    _s16693context.PracownikZespol.Remove(item);
                    await _s16693context.SaveChangesAsync();
                }

                var idS = await _s16693context.PracownikZespol.Where(x => x.IdZespol == tEM.Zespol.IdZespol).ToListAsync();
                foreach (var item in pracowniklist)
                {
                    if (idS.Contains(item))
                    {
                        _s16693context.PracownikZespol.Add(item);
                        await _s16693context.SaveChangesAsync();
                    }
                }
                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Teams));
            }

            else if (!ModelState.IsValid)
            {
                var allpracownik = await _s16693context.Pracownik
                .Include(o => o.IdPracownikNavigation)
                .Select(x => new CheckBoxItem()
                {
                    Id = x.IdPracownik,
                    Nazwa = x.IdPracownikNavigation.AdresEmail,
                    IsChecked = x.PracownikZespol.Any(x => x.IdZespol == tEM.Zespol.IdZespol) ? true : false
                }).ToListAsync();

                var newTEM = new TeamEditModel()
                {
                    Zespol = tEM.Zespol,
                    Pracowniks = allpracownik,
                };

                return View("TeamsEdit", newTEM);
            }
            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsDelete(int? id)
        {
            foreach (var delPZ in _s16693context.PracownikZespol)
            {
                if (delPZ.IdZespol == id)
                {
                    _s16693context.PracownikZespol.Remove(delPZ);
                }
            }

            var zespol = await _s16693context.Zespol.FindAsync(id);

            _s16693context.Remove(zespol);
            await _s16693context.SaveChangesAsync();

            return RedirectToAction(nameof(Teams));
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> TaskDetails(int? id1, int? id2, string data)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(data);
            var uslugaprojekt = await _s16693context.UslugaProjekt
                .Include(x => x.IdUslugaNavigation)
                .FirstOrDefaultAsync(x => x.IdProjekt == id1 & x.IdUsluga == id2 & x.DataPrzypisaniaZadania == fromDateAsDateTime);

            if (uslugaprojekt == null)
            {
                return NotFound();
            }

            return View(uslugaprojekt);
        }

        [HttpGet]
        public async Task<IActionResult> TaskCreate(int? id)
        {
            var pakiet = await _s16693context.ProjektPakiet.FirstOrDefaultAsync(x => x.IdProjekt == id && x.DataZakonczeniaWspolpracy == null);
            var pU = await _s16693context.PakietUsluga.Where(x => x.IdPakiet == pakiet.IdPakiet).Include(u => u.IdUslugaNavigation).ToListAsync();
            List<Service> uslugas = new List<Service>();
            foreach (var item in pU)
            {
                uslugas.Add(await _s16693context.Usluga.FirstOrDefaultAsync(x => x.IdUsluga == item.IdUsluga));
            }

            var projekt = await _s16693context.Projekt.FindAsync(id);

            var tCM = new TaskCreateModel
            {
                Uslugas = uslugas,
                Projekt = projekt
            };

            return View(tCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskCreate(TaskCreateModel tCM)
        {
            if (ModelState.IsValid)
            {
                var newZadanie = new ServiceProject()
                {
                    IdProjekt = tCM.Projekt.IdProjekt,
                    IdUsluga = tCM.UslugaProjekt.IdUsluga,
                    Opis = tCM.UslugaProjekt.Opis,
                    DataPrzypisaniaZadania = tCM.UslugaProjekt.DataPrzypisaniaZadania,
                    DataZakonczeniaZadania = tCM.UslugaProjekt.DataZakonczeniaZadania,
                    Status = tCM.UslugaProjekt.Status
                };

                _s16693context.Add(newZadanie);
                await _s16693context.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { id = tCM.Projekt.IdProjekt });
            }
            else if (!ModelState.IsValid)
            {
                var pakiet = await _s16693context.ProjektPakiet.FirstOrDefaultAsync(x => x.IdProjekt == tCM.Projekt.IdProjekt && x.DataZakonczeniaWspolpracy == null);
                var pU = await _s16693context.PakietUsluga.Where(x => x.IdPakiet == pakiet.IdPakiet).Include(u => u.IdUslugaNavigation).ToListAsync();
                List<Service> uslugas = new List<Service>();

                foreach (var item in pU)
                {
                    uslugas.Add(await _s16693context.Usluga.FirstOrDefaultAsync(x => x.IdUsluga == item.IdUsluga));
                }

                var newTCM = new TaskCreateModel
                {
                    Uslugas = uslugas,
                    Projekt = tCM.Projekt
                };

                return View("TaskCreate", newTCM);
            }
            return View(tCM);
        }

        [HttpGet]
        public async Task<IActionResult> TaskEdit(int? id1, int? id2, string data)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(data);
            var uslugaprojekt = await _s16693context.UslugaProjekt.FirstOrDefaultAsync(x => x.IdProjekt == id1 & x.IdUsluga == id2 & x.DataPrzypisaniaZadania == fromDateAsDateTime);
            if (uslugaprojekt == null)
            {
                return NotFound();
            }

            var pakiet = await _s16693context.ProjektPakiet.FirstOrDefaultAsync(x => x.IdProjekt == id1 && x.DataZakonczeniaWspolpracy == null);
            var pU = await _s16693context.PakietUsluga.Where(x => x.IdPakiet == pakiet.IdPakiet).Include(u => u.IdUslugaNavigation).ToListAsync();
            List<Service> uslugas = new List<Service>();
            foreach (var item in pU)
            {
                uslugas.Add(await _s16693context.Usluga.FirstOrDefaultAsync(x => x.IdUsluga == item.IdUsluga));
            }

            var tEM = new TaskEditModel
            {
                UslugaProjekt = uslugaprojekt,
                Uslugas = uslugas
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskEdit(TaskEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _s16693context.Update(tEM.UslugaProjekt);
                await _s16693context.SaveChangesAsync();

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
        public async Task<IActionResult> TaskDelete(int? id1, int? id2, string data)
        {
            var fromDateAsDateTime = DateTime.Parse(data);
            var zadanie = await _s16693context.UslugaProjekt.FirstOrDefaultAsync(x => x.IdProjekt == id1 & x.IdUsluga == id2 & x.DataPrzypisaniaZadania == fromDateAsDateTime);

            _s16693context.Remove(zadanie);
            await _s16693context.SaveChangesAsync();

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