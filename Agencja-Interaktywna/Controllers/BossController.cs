using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Interactive_Agency.Models;
using Interactive_Agency.Models.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interactive_Agency.Controllers
{
    [Authorize(Roles = "Szef")]
    public class BossController : Controller
    {
        private readonly InteractiveAgencyContext _s16693context = new InteractiveAgencyContext();
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

            var pr = await _s16693context.Project.ToListAsync();

            return View(pr);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _s16693context.Project
            .FirstOrDefaultAsync(e => e.ProjectId == id);

            var tasks = await _s16693context.ServiceProject
                .Include(p => p.ProjectIdNavigation)
                .Include(z => z.ServiceIdNavigation)
                .Where(e => e.ProjectId == id)
                .ToListAsync();

            var pDM = new ProjectDetailsModel
            {
                Project = project,
                Services = tasks
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
            var firma = from e in _s16693context.Company select e;
            var zespol = from e in _s16693context.Team select e;
            var pakiet = from e in _s16693context.Package select e;

            var pCM = new ProjectCreateModel
            {
                Companies = await firma.ToListAsync(),
                Teams = await zespol.ToListAsync(),
                Packages = await pakiet.ToListAsync()
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
                    ProjectName = pCM.Project.ProjectName,
                    ProjectLogo = "images/" + Path.GetFileName(pCM.FormFile.FileName),
                    CompanyId = pCM.Project.CompanyId
                };

                _s16693context.Add(newProjekt);
                await _s16693context.SaveChangesAsync();

                var newZP = new TeamProject()
                {
                    ProjectId = newProjekt.ProjectId,
                    TeamId = (int)pCM.Team.TeamId,
                    AssignStart = DateTime.Now
                };

                var newPP = new ProjectPackage()
                {
                    ProjectId = newProjekt.ProjectId,
                    PackageId = (int)pCM.Package.PackageId,
                    DealStart = DateTime.Now
                };

                _s16693context.Add(newZP);
                _s16693context.Add(newPP);

                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                var firma = from e in _s16693context.Company select e;
                var zespol = from e in _s16693context.Team select e;
                var pakiet = from e in _s16693context.Package select e;

                pCM.Teams = await zespol.ToListAsync();
                pCM.Packages = await pakiet.ToListAsync();
                pCM.Companies = await firma.ToListAsync();

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

            var projekt = await _s16693context.Project.FindAsync(id);

            if (projekt == null)
            {
                return NotFound();
            }

            var zp = await _s16693context.TeamProject.FirstOrDefaultAsync(x => x.ProjectId == projekt.ProjectId && x.AssignEnd == null);
            var pp = await _s16693context.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == projekt.ProjectId && x.DealEnd == null);
            var zespol = await _s16693context.Team.FirstOrDefaultAsync(x => x.TeamId == zp.TeamId);
            var pakiet = await _s16693context.Package.FirstOrDefaultAsync(x => x.PackageId == pp.PackageId);
            var IdZespol = zespol.TeamId;
            var IdPakiet = pakiet.PackageId;
            var firmy = from e in _s16693context.Company select e;
            var zespoly = from e in _s16693context.Team select e;
            var pakiety = from e in _s16693context.Package select e;

            var pEM = new ProjectEditModel
            {
                Project = projekt,
                Team = zespol,
                Package = pakiet,
                Companies = await firmy.ToListAsync(),
                Teams = await zespoly.ToListAsync(),
                Packages = await pakiety.ToListAsync(),
                TeamId = (int)IdZespol,
                PackageId = (int)IdPakiet
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

                _s16693context.Update(pEM.Project);

                var oldZP = await _s16693context.TeamProject
                    .Where(x => x.TeamId == pEM.TeamId && x.ProjectId == pEM.Project.ProjectId && x.AssignEnd == null)
                    .FirstOrDefaultAsync();
                var oldPP = await _s16693context.ProjectPackage
                    .Where(x => x.PackageId == pEM.PackageId && x.ProjectId == pEM.Project.ProjectId && x.DealEnd == null)
                    .FirstOrDefaultAsync();

                if (oldZP.TeamId != pEM.Team.TeamId)
                {

                    oldZP.AssignEnd = DateTime.Now;
                    _s16693context.Update(oldZP);

                    var newZP = new TeamProject()
                    {
                        ProjectId = pEM.Project.ProjectId,
                        TeamId = (int)pEM.Team.TeamId,
                        AssignStart = DateTime.Now
                    };

                    _s16693context.Add(newZP);
                    await _s16693context.SaveChangesAsync();

                }

                if (oldPP.PackageId != pEM.Package.PackageId)
                {

                    oldPP.DealEnd = DateTime.Now;
                    _s16693context.Update(oldPP);

                    var newPP = new ProjectPackage()
                    {
                        ProjectId = pEM.Project.ProjectId,
                        PackageId = (int)pEM.Package.PackageId,
                        DealStart = DateTime.Now
                    };

                    _s16693context.Add(newPP);
                    await _s16693context.SaveChangesAsync();

                }

                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else if (!ModelState.IsValid)
            {
                var firmy = from e in _s16693context.Company select e;
                var zespoly = from e in _s16693context.Team select e;
                var pakiety = from e in _s16693context.Package select e;

                var newPEM = new ProjectEditModel
                {
                    Project = pEM.Project,
                    Team = pEM.Team,
                    Package = pEM.Package,
                    Companies = await firmy.ToListAsync(),
                    Teams = await zespoly.ToListAsync(),
                    Packages = await pakiety.ToListAsync(),
                    TeamId = (int)pEM.TeamId,
                    PackageId = (int)pEM.PackageId
                };
                
                return View("ProjectEdit", newPEM);
            }
            return View(pEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectDelete(ProjectDetailsModel pDM)
        {

            foreach (TeamProject delZP in _s16693context.TeamProject)
            {
                if (delZP.ProjectId == pDM.Project.ProjectId)
                {
                    _s16693context.TeamProject.Remove(delZP);
                }
            }

            foreach (ProjectPackage delPP in _s16693context.ProjectPackage)
            {
                if (delPP.ProjectId == pDM.Project.ProjectId)
                {
                    _s16693context.ProjectPackage.Remove(delPP);
                }
            }

            foreach (ServiceProject delZadP in _s16693context.ServiceProject)
            {
                if (delZadP.ProjectId == pDM.Project.ProjectId)
                {
                    _s16693context.ServiceProject.Remove(delZadP);
                }
            }

            var projekt = await _s16693context.Project.FindAsync(pDM.Project.ProjectId);

            _s16693context.Remove(projekt);
            await _s16693context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Contract(int? id)
        {
            var contract = await _s16693context.ProjectPackage
                .Include(pr => pr.ProjectIdNavigation)
                .Include(pa => pa.PackageIdNavigation)
                .Where(x => x.ProjectId == id)
                .OrderByDescending(e => e.PackageId)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Meetings()
        {
            var meetings = await _s16693context.EmployeeClient
                .Include(k => k.ClientIdNavigation)
                    .ThenInclude(o => o.ClientIdNavigation)
                .Include(p => p.EmployeeIdNavigation)
                    .ThenInclude(po => po.EmployeeIdNavigation)
                .ToListAsync();

            return View(meetings);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingsCreate()
        {
            var klient = await _s16693context.Client.Include(o => o.ClientIdNavigation).ToListAsync();
            var pracownik = await _s16693context.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

            var mCM = new MeetingCreateModel
            {
                Clients = klient,
                Employees = pracownik
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
                    MeetingLocation = mCM.EmployeeClient.MeetingLocation,
                    MeetingStart = mCM.EmployeeClient.MeetingStart,
                    MeetingEnd = mCM.EmployeeClient.MeetingEnd,
                    EmployeeId = mCM.EmployeeClient.EmployeeId,
                    ClientId = mCM.EmployeeClient.ClientId
                };

                _s16693context.Add(newSpotkanie);
                await _s16693context.SaveChangesAsync();

                return RedirectToAction(nameof(Meetings));
            }
            else if (!ModelState.IsValid)
            {
                var klient = await _s16693context.Client.Include(o => o.ClientIdNavigation).ToListAsync();
                var pracownik = await _s16693context.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

                var newMCM = new MeetingCreateModel
                {
                    Clients = klient,
                    Employees = pracownik
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
            var spotkanie = await _s16693context.EmployeeClient.FirstOrDefaultAsync(x => x.ClientId == id1 & x.EmployeeId == id2 & x.MeetingStart == fromDateAsDateTime);
            if (spotkanie == null)
            {
                return NotFound();
            }

            var klients = await _s16693context.Client.Include(o => o.ClientIdNavigation).ToListAsync();
            var pracowniks = await _s16693context.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

            var mEM = new MeetingEditModel
            {
                EmployeeClient = spotkanie,
                EmployeeId = (int)spotkanie.EmployeeId,
                ClientId = (int)spotkanie.ClientId,
                MeetingStart = (DateTime)spotkanie.MeetingStart,
                Clients = klients,
                Employees = pracowniks
            };

            return View(mEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsEdit(MeetingEditModel mEM)
        {
            if (ModelState.IsValid)
            {
                var oldPK = await _s16693context.EmployeeClient
                    .Where(x => x.EmployeeId == mEM.EmployeeId && x.ClientId == mEM.ClientId && x.MeetingStart == mEM.MeetingStart)
                    .FirstOrDefaultAsync();

                if (mEM.EmployeeClient.EmployeeId != oldPK.EmployeeId || mEM.EmployeeClient.ClientId != oldPK.ClientId || mEM.EmployeeClient.MeetingStart != oldPK.MeetingStart)
                {
                    _s16693context.Remove(oldPK);

                    var newPK = new EmployeeClient()
                    {
                        MeetingLocation = mEM.EmployeeClient.MeetingLocation,
                        MeetingStart = mEM.EmployeeClient.MeetingStart,
                        MeetingEnd = mEM.EmployeeClient.MeetingEnd,
                        EmployeeId = mEM.EmployeeClient.EmployeeId,
                        ClientId = mEM.EmployeeClient.ClientId
                    };

                    _s16693context.Add(newPK);
                    await _s16693context.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }
                else
                {
                    _s16693context.Entry(oldPK).State = EntityState.Detached;
                    _s16693context.Update(mEM.EmployeeClient);
                    await _s16693context.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }

            }
            else if (!ModelState.IsValid)
            {

                var klients = await _s16693context.Client.Include(o => o.ClientIdNavigation).ToListAsync();
                var pracowniks = await _s16693context.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

                var newMEM = new MeetingEditModel
                {
                    EmployeeClient = mEM.EmployeeClient,
                    EmployeeId = (int)mEM.EmployeeId,
                    ClientId = (int)mEM.ClientId,
                    MeetingStart = (DateTime)mEM.MeetingStart,
                    Clients = klients,
                    Employees = pracowniks
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
            var spotkanie = await _s16693context.EmployeeClient.FirstOrDefaultAsync(x => x.ClientId == id1 & x.EmployeeId == id2 & x.MeetingStart == fromDateAsDateTime);

            _s16693context.Remove(spotkanie);
            await _s16693context.SaveChangesAsync();

            return RedirectToAction(nameof(Meetings));
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var sz = await _s16693context.Employee
                .Include(o => o.EmployeeIdNavigation)
                .FirstOrDefaultAsync(i => i.EmployeeIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

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
            var teams = await _s16693context.Team.ToListAsync();

            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> Team(int? id, string view)
        {
            if (view.Equals("Project"))
            {
                var team = await _s16693context.TeamProject
                    .Include(p => p.ProjectIdNavigation)
                    .Include(z => z.TeamIdNavigation)
                    .FirstOrDefaultAsync(x => x.ProjectId == id && x.AssignEnd == null);


                var members = await _s16693context.EmployeeTeam
                    .Include(z => z.TeamIdNavigation)
                    .Include(p => p.EmployeeIdNavigation)
                        .ThenInclude(o => o.EmployeeIdNavigation)
                        .Where(x => x.TeamId == team.TeamId)
                        .ToListAsync();
                return View(members);
            }
            else if (view.Equals("Teams"))
            {
                var members = await _s16693context.EmployeeTeam
                    .Include(z => z.TeamIdNavigation)
                    .Include(p => p.EmployeeIdNavigation)
                        .ThenInclude(o => o.EmployeeIdNavigation)
                        .Where(x => x.TeamId == id)
                        .ToListAsync();
                return View(members);
            }

            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult> TeamsCreate()
        {
            var pracownik = await _s16693context.Employee
                .Include(o => o.EmployeeIdNavigation)
                .Select(x => new SelectListItem()
                {
                    Text = x.EmployeeIdNavigation.EmailAddress,
                    Value = x.EmployeeId.ToString()
                }).ToListAsync();

            var tCM = new TeamCreateModel
            {
                Employees = pracownik
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
                    TeamName = tCM.Team.TeamName
                };

                var pracownikIds = tCM.Employees.Where(x => x.Selected).Select(y => y.Value);

                if (pracownikIds.Count() == 0)
                {
                    var pracownik = await _s16693context.Employee
                        .Include(o => o.EmployeeIdNavigation)
                        .Select(x => new SelectListItem()
                        {
                            Text = x.EmployeeIdNavigation.EmailAddress,
                            Value = x.EmployeeId.ToString()
                        }).ToListAsync();

                    tCM.Employees = pracownik;

                    return View("TeamsCreate", tCM);
                }

                _s16693context.Add(newTeam);
                await _s16693context.SaveChangesAsync();

                foreach (var id in pracownikIds)
                {
                    var PZ = new EmployeeTeam()
                    {
                        EmployeeId = int.Parse(id),
                        TeamId = (int)newTeam.TeamId,
                        AssignStart = DateTime.Now
                    };
                    _s16693context.Add(PZ);
                }

                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Teams));
            }
            else if (!ModelState.IsValid)
            {
                var pracownik = await _s16693context.Employee
                .Include(o => o.EmployeeIdNavigation)
                .Select(x => new SelectListItem()
                {
                    Text = x.EmployeeIdNavigation.EmailAddress,
                    Value = x.EmployeeId.ToString()
                }).ToListAsync();

                var newTCM = new TeamCreateModel
                {
                    Employees = pracownik
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


            var zespol = await _s16693context.Team
                .Include(z => z.EmployeeTeam)
                .ThenInclude(p => p.EmployeeIdNavigation).AsNoTracking()
                .SingleOrDefaultAsync(z => z.TeamId == id);

            if (zespol == null)
            {
                return NotFound();
            }

            var allpracownik = await _s16693context.Employee
                .Include(o => o.EmployeeIdNavigation)
                .Select(x => new CheckBoxItem()
                {
                    Id = x.EmployeeId,
                    Name = x.EmployeeIdNavigation.EmailAddress,
                    IsChecked = x.EmployeeTeam.Any(x => x.TeamId == zespol.TeamId) ? true : false
                }).ToListAsync();

            var tEM = new TeamEditModel()
            {
                Team = zespol,
                Employees = allpracownik,
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsEdit(TeamEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _s16693context.Update(tEM.Team);
                List<EmployeeTeam> pracowniklist = new List<EmployeeTeam>();

                foreach (var item in tEM.Employees)
                {
                    if (item.IsChecked == true)
                    {
                        var PZ = new EmployeeTeam()
                        {
                            EmployeeId = item.Id,
                            TeamId = (int)tEM.Team.TeamId,
                            AssignStart = DateTime.Now
                        };
                        _s16693context.Add(PZ);
                    }
                }

                var dt = await _s16693context.EmployeeTeam.Where(x => x.TeamId == tEM.Team.TeamId).ToListAsync();
                foreach (var item in dt)
                {
                    _s16693context.EmployeeTeam.Remove(item);
                    await _s16693context.SaveChangesAsync();
                }

                var idS = await _s16693context.EmployeeTeam.Where(x => x.TeamId == tEM.Team.TeamId).ToListAsync();
                foreach (var item in pracowniklist)
                {
                    if (idS.Contains(item))
                    {
                        _s16693context.EmployeeTeam.Add(item);
                        await _s16693context.SaveChangesAsync();
                    }
                }
                await _s16693context.SaveChangesAsync();
                return RedirectToAction(nameof(Teams));
            }

            else if (!ModelState.IsValid)
            {
                var allpracownik = await _s16693context.Employee
                .Include(o => o.EmployeeIdNavigation)
                .Select(x => new CheckBoxItem()
                {
                    Id = x.EmployeeId,
                    Name = x.EmployeeIdNavigation.EmailAddress,
                    IsChecked = x.EmployeeTeam.Any(x => x.TeamId == tEM.Team.TeamId) ? true : false
                }).ToListAsync();

                var newTEM = new TeamEditModel()
                {
                    Team = tEM.Team,
                    Employees = allpracownik,
                };

                return View("TeamsEdit", newTEM);
            }
            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsDelete(int? id)
        {
            foreach (var delPZ in _s16693context.EmployeeTeam)
            {
                if (delPZ.TeamId == id)
                {
                    _s16693context.EmployeeTeam.Remove(delPZ);
                }
            }

            var zespol = await _s16693context.Team.FindAsync(id);

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
            var uslugaprojekt = await _s16693context.ServiceProject
                .Include(x => x.ServiceIdNavigation)
                .FirstOrDefaultAsync(x => x.ProjectId == id1 & x.ServiceId == id2 & x.AssignStart == fromDateAsDateTime);

            if (uslugaprojekt == null)
            {
                return NotFound();
            }

            return View(uslugaprojekt);
        }

        [HttpGet]
        public async Task<IActionResult> TaskCreate(int? id)
        {
            var pakiet = await _s16693context.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == id && x.DealEnd == null);
            var pU = await _s16693context.PackageService.Where(x => x.PackageId == pakiet.PackageId).Include(u => u.ServiceIdNavigation).ToListAsync();
            List<Service> uslugas = new List<Service>();
            foreach (var item in pU)
            {
                uslugas.Add(await _s16693context.Service.FirstOrDefaultAsync(x => x.ServiceId == item.ServiceId));
            }

            var projekt = await _s16693context.Project.FindAsync(id);

            var tCM = new TaskCreateModel
            {
                Services = uslugas,
                Project = projekt
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
                    ProjectId = tCM.Project.ProjectId,
                    ServiceId = tCM.ServiceProject.ServiceId,
                    Description = tCM.ServiceProject.Description,
                    AssignStart = tCM.ServiceProject.AssignStart,
                    AssignEnd = tCM.ServiceProject.AssignEnd,
                    Status = tCM.ServiceProject.Status
                };

                _s16693context.Add(newZadanie);
                await _s16693context.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { id = tCM.Project.ProjectId });
            }
            else if (!ModelState.IsValid)
            {
                var pakiet = await _s16693context.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == tCM.Project.ProjectId && x.DealEnd == null);
                var pU = await _s16693context.PackageService.Where(x => x.PackageId == pakiet.PackageId).Include(u => u.ServiceIdNavigation).ToListAsync();
                List<Service> uslugas = new List<Service>();

                foreach (var item in pU)
                {
                    uslugas.Add(await _s16693context.Service.FirstOrDefaultAsync(x => x.ServiceId == item.ServiceId));
                }

                var newTCM = new TaskCreateModel
                {
                    Services = uslugas,
                    Project = tCM.Project
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
            var uslugaprojekt = await _s16693context.ServiceProject.FirstOrDefaultAsync(x => x.ProjectId == id1 & x.ServiceId == id2 & x.AssignStart == fromDateAsDateTime);
            if (uslugaprojekt == null)
            {
                return NotFound();
            }

            var pakiet = await _s16693context.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == id1 && x.DealEnd == null);
            var pU = await _s16693context.PackageService.Where(x => x.PackageId == pakiet.PackageId).Include(u => u.ServiceIdNavigation).ToListAsync();
            List<Service> uslugas = new List<Service>();
            foreach (var item in pU)
            {
                uslugas.Add(await _s16693context.Service.FirstOrDefaultAsync(x => x.ServiceId == item.ServiceId));
            }

            var tEM = new TaskEditModel
            {
                ServiceProject = uslugaprojekt,
                Services = uslugas
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskEdit(TaskEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _s16693context.Update(tEM.ServiceProject);
                await _s16693context.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { id = tEM.ServiceProject.ProjectId });

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
            var zadanie = await _s16693context.ServiceProject.FirstOrDefaultAsync(x => x.ProjectId == id1 & x.ServiceId == id2 & x.AssignStart == fromDateAsDateTime);

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