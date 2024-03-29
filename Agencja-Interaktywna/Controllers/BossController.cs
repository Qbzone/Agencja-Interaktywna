﻿using System;
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

            var projects = await _interactiveAgencyContext.Project.ToListAsync();

            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectDetails(int? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var project = await _interactiveAgencyContext.Project
                .FirstOrDefaultAsync(e => e.ProjectId == projectId);
            var tasks = await _interactiveAgencyContext.ServiceProject
                .Include(pr => pr.ProjectIdNavigation)
                .Include(se => se.ServiceIdNavigation)
                .Where(e => e.ProjectId == projectId)
                .ToListAsync();
            var projectDetails = new ProjectDetailsModel
            {
                Project = project,
                Services = tasks
            };

            return project == null ? NotFound() : View(projectDetails);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectCreate()
        {
            var companies = from e in _interactiveAgencyContext.Company select e;
            var teams = from e in _interactiveAgencyContext.Team select e;
            var packages = from e in _interactiveAgencyContext.Package select e;
            var projectCreate = new ProjectCreateModel
            {
                Companies = await companies.ToListAsync(),
                Teams = await teams.ToListAsync(),
                Packages = await packages.ToListAsync()
            };

            return View(projectCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> ProjectCreate(ProjectCreateModel projectCreate)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.Combine(hostingEnvironment.WebRootPath + "/images", Path.GetFileName(projectCreate.FormFile.FileName));

                projectCreate.FormFile.CopyTo(new FileStream(fileName, FileMode.Create));

                var newProject = new Project()
                {
                    ProjectName = projectCreate.Project.ProjectName,
                    ProjectLogo = "images/" + Path.GetFileName(projectCreate.FormFile.FileName),
                    CompanyId = projectCreate.Project.CompanyId
                };

                _interactiveAgencyContext.Add(newProject);
                await _interactiveAgencyContext.SaveChangesAsync();

                var newTeamProject = new TeamProject()
                {
                    ProjectId = newProject.ProjectId,
                    TeamId = (int)projectCreate.Team.TeamId,
                    AssignStart = DateTime.Now
                };
                var newProjectPackage = new ProjectPackage()
                {
                    ProjectId = newProject.ProjectId,
                    PackageId = (int)projectCreate.Package.PackageId,
                    DealStart = DateTime.Now
                };

                _interactiveAgencyContext.Add(newTeamProject);
                _interactiveAgencyContext.Add(newProjectPackage);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                var companies = from e in _interactiveAgencyContext.Company select e;
                var teams = from e in _interactiveAgencyContext.Team select e;
                var packages = from e in _interactiveAgencyContext.Package select e;

                projectCreate.Teams = await teams.ToListAsync();
                projectCreate.Packages = await packages.ToListAsync();
                projectCreate.Companies = await companies.ToListAsync();

                return View("ProjectCreate", projectCreate);
            }

            return View(projectCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ProjectEdit(int? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var project = await _interactiveAgencyContext.Project.FindAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            var teamProject = await _interactiveAgencyContext.TeamProject
                .FirstOrDefaultAsync(e => e.ProjectId == project.ProjectId && e.AssignEnd == null);
            var projectPackage = await _interactiveAgencyContext.ProjectPackage
                .FirstOrDefaultAsync(e => e.ProjectId == project.ProjectId && e.DealEnd == null);
            var team = await _interactiveAgencyContext.Team.FirstOrDefaultAsync(e => e.TeamId == teamProject.TeamId);
            var package = await _interactiveAgencyContext.Package.FirstOrDefaultAsync(e => e.PackageId == projectPackage.PackageId);
            var teamId = team.TeamId;
            var packageId = package.PackageId;
            var companies = from e in _interactiveAgencyContext.Company select e;
            var teams = from e in _interactiveAgencyContext.Team select e;
            var packages = from e in _interactiveAgencyContext.Package select e;
            var projectEdit = new ProjectEditModel
            {
                Project = project,
                Team = team,
                Package = package,
                Companies = await companies.ToListAsync(),
                Teams = await teams.ToListAsync(),
                Packages = await packages.ToListAsync(),
                TeamId = (int)teamId,
                PackageId = (int)packageId
            };

            return View(projectEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> ProjectEdit(ProjectEditModel projectEdit)
        {
            if (ModelState.IsValid)
            {
                _interactiveAgencyContext.Update(projectEdit.Project);

                var oldTeamProject = await _interactiveAgencyContext.TeamProject
                    .Where(e => e.TeamId == projectEdit.TeamId && e.ProjectId == projectEdit.Project.ProjectId && e.AssignEnd == null)
                    .FirstOrDefaultAsync();
                var oldProjectPackage = await _interactiveAgencyContext.ProjectPackage
                    .Where(e => e.PackageId == projectEdit.PackageId && e.ProjectId == projectEdit.Project.ProjectId && e.DealEnd == null)
                    .FirstOrDefaultAsync();

                if (oldTeamProject.TeamId != projectEdit.Team.TeamId)
                {
                    oldTeamProject.AssignEnd = DateTime.Now;

                    _interactiveAgencyContext.Update(oldTeamProject);

                    var newTeamProject = new TeamProject()
                    {
                        ProjectId = projectEdit.Project.ProjectId,
                        TeamId = (int)projectEdit.Team.TeamId,
                        AssignStart = DateTime.Now
                    };

                    _interactiveAgencyContext.Add(newTeamProject);
                    await _interactiveAgencyContext.SaveChangesAsync();
                }

                if (oldProjectPackage.PackageId != projectEdit.Package.PackageId)
                {
                    oldProjectPackage.DealEnd = DateTime.Now;

                    _interactiveAgencyContext.Update(oldProjectPackage);

                    var newProjectPackage = new ProjectPackage()
                    {
                        ProjectId = projectEdit.Project.ProjectId,
                        PackageId = (int)projectEdit.Package.PackageId,
                        DealStart = DateTime.Now
                    };

                    _interactiveAgencyContext.Add(newProjectPackage);
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
                var newProjectEdit = new ProjectEditModel
                {
                    Project = projectEdit.Project,
                    Team = projectEdit.Team,
                    Package = projectEdit.Package,
                    Companies = await companies.ToListAsync(),
                    Teams = await teams.ToListAsync(),
                    Packages = await packages.ToListAsync(),
                    TeamId = (int)projectEdit.TeamId,
                    PackageId = (int)projectEdit.PackageId
                };

                return View("ProjectEdit", newProjectEdit);
            }

            return View(projectEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectDelete(ProjectDetailsModel projectDetailsModel)
        {
            foreach (TeamProject delTeamProject in _interactiveAgencyContext.TeamProject)
            {
                if (delTeamProject.ProjectId == projectDetailsModel.Project.ProjectId)
                {
                    _interactiveAgencyContext.TeamProject.Remove(delTeamProject);
                }
            }

            foreach (ProjectPackage delProjectPackage in _interactiveAgencyContext.ProjectPackage)
            {
                if (delProjectPackage.ProjectId == projectDetailsModel.Project.ProjectId)
                {
                    _interactiveAgencyContext.ProjectPackage.Remove(delProjectPackage);
                }
            }

            foreach (ServiceProject delServiceProject in _interactiveAgencyContext.ServiceProject)
            {
                if (delServiceProject.ProjectId == projectDetailsModel.Project.ProjectId)
                {
                    _interactiveAgencyContext.ServiceProject.Remove(delServiceProject);
                }
            }

            var project = await _interactiveAgencyContext.Project.FindAsync(projectDetailsModel.Project.ProjectId);

            _interactiveAgencyContext.Remove(project);
            await _interactiveAgencyContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Contract(int? projectId)
        {
            var contract = await _interactiveAgencyContext.ProjectPackage
                .Include(pr => pr.ProjectIdNavigation)
                .Include(pa => pa.PackageIdNavigation)
                .Where(e => e.ProjectId == projectId)
                .OrderByDescending(e => e.PackageId)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Meetings()
        {
            var meetings = await _interactiveAgencyContext.EmployeeClient
                .Include(cl => cl.ClientIdNavigation)
                    .ThenInclude(cli => cli.ClientIdNavigation)
                .Include(em => em.EmployeeIdNavigation)
                    .ThenInclude(emp => emp.EmployeeIdNavigation)
                .ToListAsync();

            return View(meetings);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingsCreate()
        {
            var clients = await _interactiveAgencyContext.Client.Include(cl => cl.ClientIdNavigation).ToListAsync();
            var employees = await _interactiveAgencyContext.Employee.Include(em => em.EmployeeIdNavigation).ToListAsync();
            var meetingCreate = new MeetingCreateModel
            {
                Clients = clients,
                Employees = employees
            };

            return View(meetingCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsCreate(MeetingCreateModel meetingCreateModel)
        {
            if (ModelState.IsValid)
            {
                var newMeeting = new EmployeeClient()
                {
                    MeetingLocation = meetingCreateModel.EmployeeClient.MeetingLocation,
                    MeetingStart = meetingCreateModel.EmployeeClient.MeetingStart,
                    MeetingEnd = meetingCreateModel.EmployeeClient.MeetingEnd,
                    EmployeeId = meetingCreateModel.EmployeeClient.EmployeeId,
                    ClientId = meetingCreateModel.EmployeeClient.ClientId
                };

                _interactiveAgencyContext.Add(newMeeting);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction(nameof(Meetings));
            }
            else if (!ModelState.IsValid)
            {
                var clients = await _interactiveAgencyContext.Client.Include(cl => cl.ClientIdNavigation).ToListAsync();
                var employees = await _interactiveAgencyContext.Employee.Include(em => em.EmployeeIdNavigation).ToListAsync();
                var newMeetingCreate = new MeetingCreateModel
                {
                    Clients = clients,
                    Employees = employees
                };

                return View("MeetingsCreate", newMeetingCreate);
            }

            return View(meetingCreateModel);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingsEdit(int? clientId, int? employeeId, string date)
        {
            if (clientId == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(date);
            var meeting = await _interactiveAgencyContext.EmployeeClient
                .FirstOrDefaultAsync(x => x.ClientId == clientId & x.EmployeeId == employeeId & x.MeetingStart == fromDateAsDateTime);

            if (meeting == null)
            {
                return NotFound();
            }

            var clients = await _interactiveAgencyContext.Client.Include(cl => cl.ClientIdNavigation).ToListAsync();
            var employees = await _interactiveAgencyContext.Employee.Include(em => em.EmployeeIdNavigation).ToListAsync();
            var meetingEdit = new MeetingEditModel
            {
                EmployeeClient = meeting,
                EmployeeId = (int)meeting.EmployeeId,
                ClientId = (int)meeting.ClientId,
                MeetingStart = (DateTime)meeting.MeetingStart,
                Clients = clients,
                Employees = employees
            };

            return View(meetingEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsEdit(MeetingEditModel meetingEditModel)
        {
            if (ModelState.IsValid)
            {
                var oldMeeting = await _interactiveAgencyContext.EmployeeClient
                    .Where(e => e.EmployeeId == meetingEditModel.EmployeeId && e.ClientId == meetingEditModel.ClientId
                        && e.MeetingStart == meetingEditModel.MeetingStart)
                    .FirstOrDefaultAsync();

                if (meetingEditModel.EmployeeClient.EmployeeId != oldMeeting.EmployeeId
                        || meetingEditModel.EmployeeClient.ClientId != oldMeeting.ClientId
                        || meetingEditModel.EmployeeClient.MeetingStart != oldMeeting.MeetingStart)
                {
                    _interactiveAgencyContext.Remove(oldMeeting);

                    var newMeeting = new EmployeeClient()
                    {
                        MeetingLocation = meetingEditModel.EmployeeClient.MeetingLocation,
                        MeetingStart = meetingEditModel.EmployeeClient.MeetingStart,
                        MeetingEnd = meetingEditModel.EmployeeClient.MeetingEnd,
                        EmployeeId = meetingEditModel.EmployeeClient.EmployeeId,
                        ClientId = meetingEditModel.EmployeeClient.ClientId
                    };

                    _interactiveAgencyContext.Add(newMeeting);
                    await _interactiveAgencyContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }
                else
                {
                    _interactiveAgencyContext.Entry(oldMeeting).State = EntityState.Detached;
                    _interactiveAgencyContext.Update(meetingEditModel.EmployeeClient);
                    await _interactiveAgencyContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Meetings));
                }
            }
            else if (!ModelState.IsValid)
            {
                var clients = await _interactiveAgencyContext.Client.Include(cl => cl.ClientIdNavigation).ToListAsync();
                var employees = await _interactiveAgencyContext.Employee.Include(em => em.EmployeeIdNavigation).ToListAsync();
                var newMeetingEdit = new MeetingEditModel
                {
                    EmployeeClient = meetingEditModel.EmployeeClient,
                    EmployeeId = (int)meetingEditModel.EmployeeId,
                    ClientId = (int)meetingEditModel.ClientId,
                    MeetingStart = (DateTime)meetingEditModel.MeetingStart,
                    Clients = clients,
                    Employees = employees
                };

                return View("MeetingsEdit", newMeetingEdit);
            }

            return View(meetingEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingsDelete(int? clientId, int? employeeId, string date)
        {
            var fromDateAsDateTime = DateTime.Parse(date);
            var meeting = await _interactiveAgencyContext.EmployeeClient
                .FirstOrDefaultAsync(x => x.ClientId == clientId & x.EmployeeId == employeeId & x.MeetingStart == fromDateAsDateTime);

            _interactiveAgencyContext.Remove(meeting);
            await _interactiveAgencyContext.SaveChangesAsync();

            return RedirectToAction(nameof(Meetings));
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var boss = await _interactiveAgencyContext.Employee
                .Include(em => em.EmployeeIdNavigation)
                .FirstOrDefaultAsync(e => e.EmployeeIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            return boss == null ? NotFound() : View(boss);
        }

        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            var teams = await _interactiveAgencyContext.Team.ToListAsync();

            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> Team(int? teamId, string view)
        {
            if (view.Equals("Project"))
            {
                var team = await _interactiveAgencyContext.TeamProject
                    .Include(pr => pr.ProjectIdNavigation)
                    .Include(te => te.TeamIdNavigation)
                    .FirstOrDefaultAsync(e => e.ProjectId == teamId && e.AssignEnd == null);
                var members = await _interactiveAgencyContext.EmployeeTeam
                    .Include(te => te.TeamIdNavigation)
                    .Include(em => em.EmployeeIdNavigation)
                        .ThenInclude(emp => emp.EmployeeIdNavigation)
                        .Where(e => e.TeamId == team.TeamId)
                        .ToListAsync();

                return View(members);
            }
            else if (view.Equals("Teams"))
            {
                var members = await _interactiveAgencyContext.EmployeeTeam
                    .Include(te => te.TeamIdNavigation)
                    .Include(em => em.EmployeeIdNavigation)
                        .ThenInclude(emp => emp.EmployeeIdNavigation)
                        .Where(e => e.TeamId == teamId)
                        .ToListAsync();

                return View(members);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> TeamsCreate()
        {
            var employee = await _interactiveAgencyContext.Employee
                .Include(em => em.EmployeeIdNavigation)
                .Select(e => new SelectListItem()
                {
                    Text = e.EmployeeIdNavigation.EmailAddress,
                    Value = e.EmployeeId.ToString()
                }).ToListAsync();
            var teamCreate = new TeamCreateModel
            {
                Employees = employee
            };

            return View(teamCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsCreate(TeamCreateModel teamCreateModel)
        {
            if (teamCreateModel.Team.TeamName == null || teamCreateModel.Team.TeamName == "")
            {
                ModelState.AddModelError("TeamErrorHandler", "Please insert team name.");
            }
            if (_interactiveAgencyContext.Team.Where(e => e.TeamName == teamCreateModel.Team.TeamName).Any())
            {
                ModelState.AddModelError("TeamErrorHandler", "Provided team name already exists. Please enter new one.");
            }
            if (!teamCreateModel.Employees.Where(x => x.Selected).Select(y => y.Value).Any())
            {
                ModelState.AddModelError("Employees", "Please select at least one employee.");
            }

            if (ModelState.IsValid)
            {
                var newTeam = new Team()
                {
                    TeamName = teamCreateModel.Team.TeamName
                };
                var employeeIds = teamCreateModel.Employees.Where(e => e.Selected).Select(f => f.Value);

                if (!employeeIds.Any())
                {
                    var employees = await _interactiveAgencyContext.Employee
                        .Include(em => em.EmployeeIdNavigation)
                        .Select(e => new SelectListItem()
                        {
                            Text = e.EmployeeIdNavigation.EmailAddress,
                            Value = e.EmployeeId.ToString()
                        }).ToListAsync();

                    teamCreateModel.Employees = employees;

                    return View("TeamsCreate", teamCreateModel);
                }

                _interactiveAgencyContext.Add(newTeam);
                await _interactiveAgencyContext.SaveChangesAsync();

                foreach (var id in employeeIds)
                {
                    var employeeTeam = new EmployeeTeam()
                    {
                        EmployeeId = int.Parse(id),
                        TeamId = (int)newTeam.TeamId,
                        AssignStart = DateTime.Now
                    };

                    _interactiveAgencyContext.Add(employeeTeam);
                }

                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction(nameof(Teams));
            }
            else if (!ModelState.IsValid)
            {
                var employees = await _interactiveAgencyContext.Employee
                    .Include(em => em.EmployeeIdNavigation)
                    .Select(e => new SelectListItem()
                    {
                        Text = e.EmployeeIdNavigation.EmailAddress,
                        Value = e.EmployeeId.ToString()
                    }).ToListAsync();

                var newTeamCreate = new TeamCreateModel
                {
                    Employees = employees
                };

                return View("TeamsCreate", newTeamCreate);
            }

            return View(teamCreateModel);
        }

        [HttpGet]
        public async Task<IActionResult> TeamsEdit(int? teamId)
        {
            if (teamId == null)
            {
                return NotFound();
            }

            var team = await _interactiveAgencyContext.Team
                .Include(et => et.EmployeeTeam)
                    .ThenInclude(em => em.EmployeeIdNavigation).AsNoTracking()
                .SingleOrDefaultAsync(e => e.TeamId == teamId);

            if (team == null)
            {
                return NotFound();
            }

            var allEmployees = await _interactiveAgencyContext.Employee
                .Include(em => em.EmployeeIdNavigation)
                .Select(e => new CheckBoxItem()
                {
                    Id = e.EmployeeId,
                    Name = e.EmployeeIdNavigation.EmailAddress,
                    IsChecked = e.EmployeeTeam.Any(x => x.TeamId == team.TeamId)
                }).ToListAsync();

            var teamEdit = new TeamEditModel()
            {
                Team = team,
                Employees = allEmployees
            };

            return View(teamEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsEdit(TeamEditModel teamEditModel)
        {
            if (teamEditModel.Team.TeamName == null || teamEditModel.Team.TeamName == "")
            {
                ModelState.AddModelError("TeamErrorHandler", "Please insert team name.");
            }
            if (_interactiveAgencyContext.Team.Where(e => e.TeamId != teamEditModel.Team.TeamId && e.TeamName == teamEditModel.Team.TeamName).Any())
            {
                ModelState.AddModelError("TeamErrorHandler", "Provided team name already exists. Please enter new one.");
            }
            if (!teamEditModel.Employees.Where(x => x.IsChecked).Any())
            {
                ModelState.AddModelError("Employees", "Please select at least one employee.");
            }

            if (ModelState.IsValid)
            {
                _interactiveAgencyContext.Update(teamEditModel.Team);

                List<EmployeeTeam> employeeList = new List<EmployeeTeam>();

                foreach (var item in teamEditModel.Employees)
                {
                    if (item.IsChecked == true)
                    {
                        var employeeTeam = new EmployeeTeam()
                        {
                            EmployeeId = item.Id,
                            TeamId = (int)teamEditModel.Team.TeamId,
                            AssignStart = DateTime.Now
                        };

                        _interactiveAgencyContext.Add(employeeTeam);
                    }
                }

                var delEmployee = await _interactiveAgencyContext.EmployeeTeam.Where(e => e.TeamId == teamEditModel.Team.TeamId).ToListAsync();

                foreach (var item in delEmployee)
                {
                    _interactiveAgencyContext.EmployeeTeam.Remove(item);
                    await _interactiveAgencyContext.SaveChangesAsync();
                }

                var addEmployee = await _interactiveAgencyContext.EmployeeTeam.Where(e => e.TeamId == teamEditModel.Team.TeamId).ToListAsync();

                foreach (var item in employeeList)
                {
                    if (addEmployee.Contains(item))
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
                    .Include(em => em.EmployeeIdNavigation)
                    .Select(e => new CheckBoxItem()
                    {
                        Id = e.EmployeeId,
                        Name = e.EmployeeIdNavigation.EmailAddress,
                        IsChecked = e.EmployeeTeam.Any(f => f.TeamId == teamEditModel.Team.TeamId)
                    }).ToListAsync();
                var newTeamEdit = new TeamEditModel()
                {
                    Team = teamEditModel.Team,
                    Employees = allEmployees
                };

                return View("TeamsEdit", newTeamEdit);
            }

            return View(teamEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamsDelete(int? teamId)
        {
            if (!_interactiveAgencyContext.TeamProject.Where(e => e.TeamId == teamId).Any())
            {
                foreach (var delEmployee in _interactiveAgencyContext.EmployeeTeam)
                {
                    if (delEmployee.TeamId == teamId)
                    {
                        _interactiveAgencyContext.EmployeeTeam.Remove(delEmployee);
                    }
                }

                var team = await _interactiveAgencyContext.Team.FindAsync(teamId);

                _interactiveAgencyContext.Remove(team);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction(nameof(Teams));
            }
            else if (_interactiveAgencyContext.TeamProject.Where(e => e.TeamId == teamId && e.AssignEnd != null).Any())
            {
                foreach (var delTeamProject in _interactiveAgencyContext.TeamProject)
                {
                    if (delTeamProject.AssignEnd != null)
                    {
                        _interactiveAgencyContext.TeamProject.Remove(delTeamProject);
                    }
                }
                foreach (var delEmployee in _interactiveAgencyContext.EmployeeTeam)
                {
                    if (delEmployee.TeamId == teamId)
                    {
                        _interactiveAgencyContext.EmployeeTeam.Remove(delEmployee);
                    }
                }

                var team = await _interactiveAgencyContext.Team.FindAsync(teamId);

                _interactiveAgencyContext.Remove(team);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction(nameof(Teams));
            }

            return RedirectToAction(nameof(Teams));
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> TaskDetails(int? projectId, int? serviceId, string date)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(date);
            var serviceProject = await _interactiveAgencyContext.ServiceProject
                .Include(se => se.ServiceIdNavigation)
                .FirstOrDefaultAsync(e => e.ProjectId == projectId & e.ServiceId == serviceId & e.AssignStart == fromDateAsDateTime);

            if (serviceProject == null)
            {
                return NotFound();
            }

            return View(serviceProject);
        }

        [HttpGet]
        public async Task<IActionResult> TaskCreate(int? projectId)
        {
            var projectPackage = await _interactiveAgencyContext.ProjectPackage
                .FirstOrDefaultAsync(e => e.ProjectId == projectId && e.DealEnd == null);
            var packageServices = await _interactiveAgencyContext.PackageService
                .Where(e => e.PackageId == projectPackage.PackageId)
                .Include(se => se.ServiceIdNavigation)
                .ToListAsync();

            List<Service> tasks = new List<Service>();

            foreach (var item in packageServices)
            {
                tasks.Add(await _interactiveAgencyContext.Service.FirstOrDefaultAsync(e => e.ServiceId == item.ServiceId));
            }

            var project = await _interactiveAgencyContext.Project.FindAsync(projectId);
            var taskCreate = new TaskCreateModel
            {
                Services = tasks,
                Project = project
            };

            return View(taskCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskCreate(TaskCreateModel taskCreateModel)
        {
            if (ModelState.IsValid)
            {
                var newTask = new ServiceProject()
                {
                    ProjectId = taskCreateModel.Project.ProjectId,
                    ServiceId = taskCreateModel.ServiceProject.ServiceId,
                    Description = taskCreateModel.ServiceProject.Description,
                    AssignStart = taskCreateModel.ServiceProject.AssignStart,
                    AssignEnd = taskCreateModel.ServiceProject.AssignEnd,
                    Status = taskCreateModel.ServiceProject.Status
                };

                _interactiveAgencyContext.Add(newTask);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { projectId = taskCreateModel.Project.ProjectId });
            }
            else if (!ModelState.IsValid)
            {
                var projectPackage = await _interactiveAgencyContext.ProjectPackage
                    .FirstOrDefaultAsync(e => e.ProjectId == taskCreateModel.Project.ProjectId && e.DealEnd == null);
                var packageServices = await _interactiveAgencyContext.PackageService
                    .Where(e => e.PackageId == projectPackage.PackageId)
                    .Include(se => se.ServiceIdNavigation)
                    .ToListAsync();

                List<Service> services = new List<Service>();

                foreach (var item in packageServices)
                {
                    services.Add(await _interactiveAgencyContext.Service.FirstOrDefaultAsync(e => e.ServiceId == item.ServiceId));
                }

                var newTaskCreate = new TaskCreateModel
                {
                    Services = services,
                    Project = taskCreateModel.Project
                };

                return View("TaskCreate", newTaskCreate);
            }

            return View(taskCreateModel);
        }

        [HttpGet]
        public async Task<IActionResult> TaskEdit(int? projectId, int? serviceId, string date)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var fromDateAsDateTime = DateTime.Parse(date);
            var serviceProject = await _interactiveAgencyContext.ServiceProject
                .FirstOrDefaultAsync(e => e.ProjectId == projectId & e.ServiceId == serviceId & e.AssignStart == fromDateAsDateTime);

            if (serviceProject == null)
            {
                return NotFound();
            }

            var projectPackage = await _interactiveAgencyContext.ProjectPackage
                .FirstOrDefaultAsync(e => e.ProjectId == projectId && e.DealEnd == null);
            var packageServices = await _interactiveAgencyContext.PackageService
                .Where(e => e.PackageId == projectPackage.PackageId)
                .Include(se => se.ServiceIdNavigation)
                .ToListAsync();

            List<Service> services = new List<Service>();

            foreach (var item in packageServices)
            {
                services.Add(await _interactiveAgencyContext.Service.FirstOrDefaultAsync(e => e.ServiceId == item.ServiceId));
            }

            var taskEditModel = new TaskEditModel
            {
                ServiceProject = serviceProject,
                Services = services
            };

            return View(taskEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskEdit(TaskEditModel taskEditModel)
        {
            if (ModelState.IsValid)
            {
                _interactiveAgencyContext.Update(taskEditModel.ServiceProject);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { projectId = taskEditModel.ServiceProject.ProjectId });
            }
            else if (!ModelState.IsValid)
            {
                return View("TaskEdit", taskEditModel);
            }

            return View(taskEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskDelete(int? projectId, int? serviceId, string date)
        {
            var fromDateAsDateTime = DateTime.Parse(date);
            var task = await _interactiveAgencyContext.ServiceProject
                .FirstOrDefaultAsync(e => e.ProjectId == projectId & e.ServiceId == serviceId & e.AssignStart == fromDateAsDateTime);

            _interactiveAgencyContext.Remove(task);
            await _interactiveAgencyContext.SaveChangesAsync();

            return RedirectToAction("ProjectDetails", new { projectId = projectId });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}