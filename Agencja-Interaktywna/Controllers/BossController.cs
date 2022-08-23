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
    [Authorize(Roles = "Boss")]
    public class BossController : Controller
    {
        private readonly InteractiveAgencyContext _interactiveAgencyContext = new InteractiveAgencyContext();
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

            var pr = await _interactiveAgencyContext.Project.ToListAsync();

            return View(pr);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _interactiveAgencyContext.Project
            .FirstOrDefaultAsync(e => e.ProjectId == id);

            var tasks = await _interactiveAgencyContext.ServiceProject
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
            var companies = from e in _interactiveAgencyContext.Company select e;
            var teams = from e in _interactiveAgencyContext.Team select e;
            var packages = from e in _interactiveAgencyContext.Package select e;

            var pCM = new ProjectCreateModel
            {
                Companies = await companies.ToListAsync(),
                Teams = await teams.ToListAsync(),
                Packages = await packages.ToListAsync()
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

                _interactiveAgencyContext.Add(newProjekt);
                await _interactiveAgencyContext.SaveChangesAsync();

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

                _interactiveAgencyContext.Add(newZP);
                _interactiveAgencyContext.Add(newPP);

                await _interactiveAgencyContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                var companies = from e in _interactiveAgencyContext.Company select e;
                var teams = from e in _interactiveAgencyContext.Team select e;
                var packages = from e in _interactiveAgencyContext.Package select e;

                pCM.Teams = await teams.ToListAsync();
                pCM.Packages = await packages.ToListAsync();
                pCM.Companies = await companies.ToListAsync();

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

            var project = await _interactiveAgencyContext.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            var zp = await _interactiveAgencyContext.TeamProject.FirstOrDefaultAsync(x => x.ProjectId == project.ProjectId && x.AssignEnd == null);
            var pp = await _interactiveAgencyContext.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == project.ProjectId && x.DealEnd == null);
            var team = await _interactiveAgencyContext.Team.FirstOrDefaultAsync(x => x.TeamId == zp.TeamId);
            var package = await _interactiveAgencyContext.Package.FirstOrDefaultAsync(x => x.PackageId == pp.PackageId);
            var TeamId = team.TeamId;
            var PackageId = package.PackageId;
            var companies = from e in _interactiveAgencyContext.Company select e;
            var teams = from e in _interactiveAgencyContext.Team select e;
            var packages = from e in _interactiveAgencyContext.Package select e;

            var pEM = new ProjectEditModel
            {
                Project = project,
                Team = team,
                Package = package,
                Companies = await companies.ToListAsync(),
                Teams = await teams.ToListAsync(),
                Packages = await packages.ToListAsync(),
                TeamId = (int)TeamId,
                PackageId = (int)PackageId
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

                _interactiveAgencyContext.Update(pEM.Project);

                var oldZP = await _interactiveAgencyContext.TeamProject
                    .Where(x => x.TeamId == pEM.TeamId && x.ProjectId == pEM.Project.ProjectId && x.AssignEnd == null)
                    .FirstOrDefaultAsync();
                var oldPP = await _interactiveAgencyContext.ProjectPackage
                    .Where(x => x.PackageId == pEM.PackageId && x.ProjectId == pEM.Project.ProjectId && x.DealEnd == null)
                    .FirstOrDefaultAsync();

                if (oldZP.TeamId != pEM.Team.TeamId)
                {

                    oldZP.AssignEnd = DateTime.Now;
                    _interactiveAgencyContext.Update(oldZP);

                    var newZP = new TeamProject()
                    {
                        ProjectId = pEM.Project.ProjectId,
                        TeamId = (int)pEM.Team.TeamId,
                        AssignStart = DateTime.Now
                    };

                    _interactiveAgencyContext.Add(newZP);
                    await _interactiveAgencyContext.SaveChangesAsync();

                }

                if (oldPP.PackageId != pEM.Package.PackageId)
                {

                    oldPP.DealEnd = DateTime.Now;
                    _interactiveAgencyContext.Update(oldPP);

                    var newPP = new ProjectPackage()
                    {
                        ProjectId = pEM.Project.ProjectId,
                        PackageId = (int)pEM.Package.PackageId,
                        DealStart = DateTime.Now
                    };

                    _interactiveAgencyContext.Add(newPP);
                    await _interactiveAgencyContext.SaveChangesAsync();

                }

                await _interactiveAgencyContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else if (!ModelState.IsValid)
            {
                var companies = from e in _interactiveAgencyContext.Company select e;
                var teams = from e in _interactiveAgencyContext.Team select e;
                var packages = from e in _interactiveAgencyContext.Package select e;

                var newPEM = new ProjectEditModel
                {
                    Project = pEM.Project,
                    Team = pEM.Team,
                    Package = pEM.Package,
                    Companies = await companies.ToListAsync(),
                    Teams = await teams.ToListAsync(),
                    Packages = await packages.ToListAsync(),
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

            foreach (TeamProject delZP in _interactiveAgencyContext.TeamProject)
            {
                if (delZP.ProjectId == pDM.Project.ProjectId)
                {
                    _interactiveAgencyContext.TeamProject.Remove(delZP);
                }
            }

            foreach (ProjectPackage delPP in _interactiveAgencyContext.ProjectPackage)
            {
                if (delPP.ProjectId == pDM.Project.ProjectId)
                {
                    _interactiveAgencyContext.ProjectPackage.Remove(delPP);
                }
            }

            foreach (ServiceProject delZadP in _interactiveAgencyContext.ServiceProject)
            {
                if (delZadP.ProjectId == pDM.Project.ProjectId)
                {
                    _interactiveAgencyContext.ServiceProject.Remove(delZadP);
                }
            }

            var project = await _interactiveAgencyContext.Project.FindAsync(pDM.Project.ProjectId);

            _interactiveAgencyContext.Remove(project);
            await _interactiveAgencyContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Contract(int? id)
        {
            var contract = await _interactiveAgencyContext.ProjectPackage
                .Include(pr => pr.ProjectIdNavigation)
                .Include(pa => pa.PackageIdNavigation)
                .Where(x => x.ProjectId == id)
                .OrderByDescending(e => e.PackageId)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Meetings()
        {
            var meetings = await _interactiveAgencyContext.EmployeeClient
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
            var clients = await _interactiveAgencyContext.Client.Include(o => o.ClientIdNavigation).ToListAsync();
            var employees = await _interactiveAgencyContext.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

            var mCM = new MeetingCreateModel
            {
                Clients = clients,
                Employees = employees
            };

            return View(mCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsCreate(MeetingCreateModel mCM)
        {
            if (ModelState.IsValid)
            {
                var newMeeting = new EmployeeClient()
                {
                    MeetingLocation = mCM.EmployeeClient.MeetingLocation,
                    MeetingStart = mCM.EmployeeClient.MeetingStart,
                    MeetingEnd = mCM.EmployeeClient.MeetingEnd,
                    EmployeeId = mCM.EmployeeClient.EmployeeId,
                    ClientId = mCM.EmployeeClient.ClientId
                };

                _interactiveAgencyContext.Add(newMeeting);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction(nameof(Meetings));
            }
            else if (!ModelState.IsValid)
            {
                var clients = await _interactiveAgencyContext.Client.Include(o => o.ClientIdNavigation).ToListAsync();
                var employees = await _interactiveAgencyContext.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

                var newMCM = new MeetingCreateModel
                {
                    Clients = clients,
                    Employees = employees
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
            var meeting = await _interactiveAgencyContext.EmployeeClient.FirstOrDefaultAsync(x => x.ClientId == id1 & x.EmployeeId == id2 
                & x.MeetingStart == fromDateAsDateTime);
            if (meeting == null)
            {
                return NotFound();
            }

            var clients = await _interactiveAgencyContext.Client.Include(o => o.ClientIdNavigation).ToListAsync();
            var employees = await _interactiveAgencyContext.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

            var mEM = new MeetingEditModel
            {
                EmployeeClient = meeting,
                EmployeeId = (int)meeting.EmployeeId,
                ClientId = (int)meeting.ClientId,
                MeetingStart = (DateTime)meeting.MeetingStart,
                Clients = clients,
                Employees = employees
            };

            return View(mEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsEdit(MeetingEditModel mEM)
        {
            if (ModelState.IsValid)
            {
                var oldPK = await _interactiveAgencyContext.EmployeeClient
                    .Where(x => x.EmployeeId == mEM.EmployeeId && x.ClientId == mEM.ClientId && x.MeetingStart == mEM.MeetingStart)
                    .FirstOrDefaultAsync();

                if (mEM.EmployeeClient.EmployeeId != oldPK.EmployeeId || mEM.EmployeeClient.ClientId != oldPK.ClientId || mEM.EmployeeClient.MeetingStart != oldPK.MeetingStart)
                {
                    _interactiveAgencyContext.Remove(oldPK);

                    var newPK = new EmployeeClient()
                    {
                        MeetingLocation = mEM.EmployeeClient.MeetingLocation,
                        MeetingStart = mEM.EmployeeClient.MeetingStart,
                        MeetingEnd = mEM.EmployeeClient.MeetingEnd,
                        EmployeeId = mEM.EmployeeClient.EmployeeId,
                        ClientId = mEM.EmployeeClient.ClientId
                    };

                    _interactiveAgencyContext.Add(newPK);
                    await _interactiveAgencyContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }
                else
                {
                    _interactiveAgencyContext.Entry(oldPK).State = EntityState.Detached;
                    _interactiveAgencyContext.Update(mEM.EmployeeClient);
                    await _interactiveAgencyContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }

            }
            else if (!ModelState.IsValid)
            {

                var clients = await _interactiveAgencyContext.Client.Include(o => o.ClientIdNavigation).ToListAsync();
                var employees = await _interactiveAgencyContext.Employee.Include(o => o.EmployeeIdNavigation).ToListAsync();

                var newMEM = new MeetingEditModel
                {
                    EmployeeClient = mEM.EmployeeClient,
                    EmployeeId = (int)mEM.EmployeeId,
                    ClientId = (int)mEM.ClientId,
                    MeetingStart = (DateTime)mEM.MeetingStart,
                    Clients = clients,
                    Employees = employees
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
            var meeting = await _interactiveAgencyContext.EmployeeClient.FirstOrDefaultAsync(x => x.ClientId == id1 & x.EmployeeId == id2 & x.MeetingStart == fromDateAsDateTime);

            _interactiveAgencyContext.Remove(meeting);
            await _interactiveAgencyContext.SaveChangesAsync();

            return RedirectToAction(nameof(Meetings));
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var sz = await _interactiveAgencyContext.Employee
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
            var teams = await _interactiveAgencyContext.Team.ToListAsync();

            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> Team(int? id, string view)
        {
            if (view.Equals("Project"))
            {
                var team = await _interactiveAgencyContext.TeamProject
                    .Include(p => p.ProjectIdNavigation)
                    .Include(z => z.TeamIdNavigation)
                    .FirstOrDefaultAsync(x => x.ProjectId == id && x.AssignEnd == null);


                var members = await _interactiveAgencyContext.EmployeeTeam
                    .Include(z => z.TeamIdNavigation)
                    .Include(p => p.EmployeeIdNavigation)
                        .ThenInclude(o => o.EmployeeIdNavigation)
                        .Where(x => x.TeamId == team.TeamId)
                        .ToListAsync();
                return View(members);
            }
            else if (view.Equals("Teams"))
            {
                var members = await _interactiveAgencyContext.EmployeeTeam
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
            var employee = await _interactiveAgencyContext.Employee
                .Include(o => o.EmployeeIdNavigation)
                .Select(x => new SelectListItem()
                {
                    Text = x.EmployeeIdNavigation.EmailAddress,
                    Value = x.EmployeeId.ToString()
                }).ToListAsync();

            var tCM = new TeamCreateModel
            {
                Employees = employee
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

                var employeeIds = tCM.Employees.Where(x => x.Selected).Select(y => y.Value);

                if (employeeIds.Count() == 0)
                {
                    var employees = await _interactiveAgencyContext.Employee
                        .Include(o => o.EmployeeIdNavigation)
                        .Select(x => new SelectListItem()
                        {
                            Text = x.EmployeeIdNavigation.EmailAddress,
                            Value = x.EmployeeId.ToString()
                        }).ToListAsync();

                    tCM.Employees = employees;

                    return View("TeamsCreate", tCM);
                }

                _interactiveAgencyContext.Add(newTeam);
                await _interactiveAgencyContext.SaveChangesAsync();

                foreach (var id in employeeIds)
                {
                    var PZ = new EmployeeTeam()
                    {
                        EmployeeId = int.Parse(id),
                        TeamId = (int)newTeam.TeamId,
                        AssignStart = DateTime.Now
                    };
                    _interactiveAgencyContext.Add(PZ);
                }

                await _interactiveAgencyContext.SaveChangesAsync();
                return RedirectToAction(nameof(Teams));
            }
            else if (!ModelState.IsValid)
            {
                var employees = await _interactiveAgencyContext.Employee
                .Include(o => o.EmployeeIdNavigation)
                .Select(x => new SelectListItem()
                {
                    Text = x.EmployeeIdNavigation.EmailAddress,
                    Value = x.EmployeeId.ToString()
                }).ToListAsync();

                var newTCM = new TeamCreateModel
                {
                    Employees = employees
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


            var team = await _interactiveAgencyContext.Team
                .Include(z => z.EmployeeTeam)
                .ThenInclude(p => p.EmployeeIdNavigation).AsNoTracking()
                .SingleOrDefaultAsync(z => z.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            var allEmployees = await _interactiveAgencyContext.Employee
                .Include(o => o.EmployeeIdNavigation)
                .Select(x => new CheckBoxItem()
                {
                    Id = x.EmployeeId,
                    Name = x.EmployeeIdNavigation.EmailAddress,
                    IsChecked = x.EmployeeTeam.Any(x => x.TeamId == team.TeamId) ? true : false
                }).ToListAsync();

            var tEM = new TeamEditModel()
            {
                Team = team,
                Employees = allEmployees,
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsEdit(TeamEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _interactiveAgencyContext.Update(tEM.Team);
                List<EmployeeTeam> employeeList = new List<EmployeeTeam>();

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
                        _interactiveAgencyContext.Add(PZ);
                    }
                }

                var dt = await _interactiveAgencyContext.EmployeeTeam.Where(x => x.TeamId == tEM.Team.TeamId).ToListAsync();
                foreach (var item in dt)
                {
                    _interactiveAgencyContext.EmployeeTeam.Remove(item);
                    await _interactiveAgencyContext.SaveChangesAsync();
                }

                var idS = await _interactiveAgencyContext.EmployeeTeam.Where(x => x.TeamId == tEM.Team.TeamId).ToListAsync();
                foreach (var item in employeeList)
                {
                    if (idS.Contains(item))
                    {
                        _interactiveAgencyContext.EmployeeTeam.Add(item);
                        await _interactiveAgencyContext.SaveChangesAsync();
                    }
                }
                await _interactiveAgencyContext.SaveChangesAsync();
                return RedirectToAction(nameof(Teams));
            }

            else if (!ModelState.IsValid)
            {
                var allEmployees = await _interactiveAgencyContext.Employee
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
                    Employees = allEmployees,
                };

                return View("TeamsEdit", newTEM);
            }
            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsDelete(int? id)
        {
            foreach (var delPZ in _interactiveAgencyContext.EmployeeTeam)
            {
                if (delPZ.TeamId == id)
                {
                    _interactiveAgencyContext.EmployeeTeam.Remove(delPZ);
                }
            }

            var zespol = await _interactiveAgencyContext.Team.FindAsync(id);

            _interactiveAgencyContext.Remove(zespol);
            await _interactiveAgencyContext.SaveChangesAsync();

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
            var serviceProject = await _interactiveAgencyContext.ServiceProject
                .Include(x => x.ServiceIdNavigation)
                .FirstOrDefaultAsync(x => x.ProjectId == id1 & x.ServiceId == id2 & x.AssignStart == fromDateAsDateTime);

            if (serviceProject == null)
            {
                return NotFound();
            }

            return View(serviceProject);
        }

        [HttpGet]
        public async Task<IActionResult> TaskCreate(int? id)
        {
            var package = await _interactiveAgencyContext.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == id && x.DealEnd == null);
            var pU = await _interactiveAgencyContext.PackageService.Where(x => x.PackageId == package.PackageId).Include(u => u.ServiceIdNavigation).ToListAsync();
            List<Service> services = new List<Service>();
            foreach (var item in pU)
            {
                services.Add(await _interactiveAgencyContext.Service.FirstOrDefaultAsync(x => x.ServiceId == item.ServiceId));
            }

            var project = await _interactiveAgencyContext.Project.FindAsync(id);

            var tCM = new TaskCreateModel
            {
                Services = services,
                Project = project
            };

            return View(tCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskCreate(TaskCreateModel tCM)
        {
            if (ModelState.IsValid)
            {
                var newTask = new ServiceProject()
                {
                    ProjectId = tCM.Project.ProjectId,
                    ServiceId = tCM.ServiceProject.ServiceId,
                    Description = tCM.ServiceProject.Description,
                    AssignStart = tCM.ServiceProject.AssignStart,
                    AssignEnd = tCM.ServiceProject.AssignEnd,
                    Status = tCM.ServiceProject.Status
                };

                _interactiveAgencyContext.Add(newTask);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { id = tCM.Project.ProjectId });
            }
            else if (!ModelState.IsValid)
            {
                var package = await _interactiveAgencyContext.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == tCM.Project.ProjectId && x.DealEnd == null);
                var pU = await _interactiveAgencyContext.PackageService.Where(x => x.PackageId == package.PackageId).Include(u => u.ServiceIdNavigation).ToListAsync();
                List<Service> services = new List<Service>();

                foreach (var item in pU)
                {
                    services.Add(await _interactiveAgencyContext.Service.FirstOrDefaultAsync(x => x.ServiceId == item.ServiceId));
                }

                var newTCM = new TaskCreateModel
                {
                    Services = services,
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
            var serviceProject = await _interactiveAgencyContext.ServiceProject.FirstOrDefaultAsync(x => x.ProjectId == id1 & x.ServiceId == id2 & x.AssignStart == fromDateAsDateTime);
            if (serviceProject == null)
            {
                return NotFound();
            }

            var package = await _interactiveAgencyContext.ProjectPackage.FirstOrDefaultAsync(x => x.ProjectId == id1 && x.DealEnd == null);
            var pU = await _interactiveAgencyContext.PackageService.Where(x => x.PackageId == package.PackageId).Include(u => u.ServiceIdNavigation).ToListAsync();
            List<Service> services = new List<Service>();
            foreach (var item in pU)
            {
                services.Add(await _interactiveAgencyContext.Service.FirstOrDefaultAsync(x => x.ServiceId == item.ServiceId));
            }

            var tEM = new TaskEditModel
            {
                ServiceProject = serviceProject,
                Services = services
            };

            return View(tEM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskEdit(TaskEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                _interactiveAgencyContext.Update(tEM.ServiceProject);
                await _interactiveAgencyContext.SaveChangesAsync();

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
            var task = await _interactiveAgencyContext.ServiceProject.FirstOrDefaultAsync(x => x.ProjectId == id1 & x.ServiceId == id2 & x.AssignStart == fromDateAsDateTime);

            _interactiveAgencyContext.Remove(task);
            await _interactiveAgencyContext.SaveChangesAsync();

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