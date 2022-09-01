using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Interactive_Agency.Models;
using Interactive_Agency.Models.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interactive_Agency.Controllers
{
    [Authorize(Roles = "Graphician")]
    public class GraphicianController : Controller
    {
        private readonly InteractiveAgencyContext _interactiveAgencyContext = new InteractiveAgencyContext();

        public async Task<IActionResult> Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var teams = await _interactiveAgencyContext.Team
                    .Include(et => et.EmployeeTeam)
                        .ThenInclude(em => em.EmployeeIdNavigation)
                            .ThenInclude(emp => emp.EmployeeIdNavigation)
                    .Where(e => e.EmployeeTeam
                        .Any(f => f.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User
                            .FindFirst(ClaimTypes.Name).Value)).ToListAsync();

            List<Project> projects = new List<Project>();

            foreach (var item in teams)
            {
                projects.AddRange(await _interactiveAgencyContext.Project
                    .Include(tp => tp.TeamProject)
                        .ThenInclude(te => te.TeamIdNavigation)
                    .Where(e => e.TeamProject.Any(f => f.TeamId == item.TeamId && f.AssignEnd == null)).ToListAsync());
            }

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
                .Where(e => e.ProjectId == projectId && e.ServiceIdNavigation.Classification == HttpContext.User.FindFirst(ClaimTypes.Role).Value)
                .ToListAsync();
            var projectDetails = new ProjectDetailsModel
            {
                Project = project,
                Services = tasks
            };

            return project == null ? NotFound() : View(projectDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            var teams = await _interactiveAgencyContext.Team
                    .Include(et => et.EmployeeTeam)
                        .ThenInclude(em => em.EmployeeIdNavigation)
                            .ThenInclude(emp => emp.EmployeeIdNavigation)
                    .Where(e => e.EmployeeTeam
                        .Any(f => f.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User
                            .FindFirst(ClaimTypes.Name).Value)).ToListAsync();

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

            var package = await _interactiveAgencyContext.ProjectPackage
                .FirstOrDefaultAsync(e => e.ProjectId == projectId && e.DealEnd == null);
            var packageServices = await _interactiveAgencyContext.PackageService
                .Where(e => e.PackageId == package.PackageId)
                .Include(se => se.ServiceIdNavigation)
                .ToListAsync();

            List<Service> services = new List<Service>();

            foreach (var item in packageServices)
            {
                services.Add(await _interactiveAgencyContext.Service.FirstOrDefaultAsync(e => e.ServiceId == item.ServiceId));
            }

            var taskEdit = new TaskEditModel
            {
                ServiceProject = serviceProject,
                Services = services
            };

            return View(taskEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskEdit(TaskEditModel taskEdit)
        {
            if (ModelState.IsValid)
            {
                _interactiveAgencyContext.Update(taskEdit.ServiceProject);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { projectId = taskEdit.ServiceProject.ProjectId });
            }
            else if (!ModelState.IsValid)
            {
                return View("TaskEdit", taskEdit);
            }

            return View(taskEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskDelay(TaskEditModel taskEdit)
        {
            if (ModelState.IsValid)
            {
                DateTime tmpDate = (DateTime)taskEdit.ServiceProject.AssignEnd;
                taskEdit.ServiceProject.AssignEnd = tmpDate.AddDays(7);
                taskEdit.ServiceProject.Status = "Delayed";

                _interactiveAgencyContext.Update(taskEdit.ServiceProject);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction("ProjectDetails", new { projectId = taskEdit.ServiceProject.ProjectId });
            }
            else if (!ModelState.IsValid)
            {
                return View("TaskEdit", taskEdit);
            }

            return View(taskEdit);
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

        public async Task<IActionResult> Meetings()
        {
            var meetings = await _interactiveAgencyContext.EmployeeClient
                .Include(cl => cl.ClientIdNavigation)
                    .ThenInclude(cli => cli.ClientIdNavigation)
                .Include(em => em.EmployeeIdNavigation)
                    .ThenInclude(emp => emp.EmployeeIdNavigation)
                .Where(e => e.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User
                    .FindFirst(ClaimTypes.Name).Value).ToListAsync();

            return View(meetings);
        }

        public async Task<IActionResult> Profile()
        {
            var meeting = await _interactiveAgencyContext.EmployeeContract
                .Include(em => em.EmployeeIdNavigation)
                    .ThenInclude(emp => emp.EmployeeIdNavigation)
                .Include(co => co.ContractIdNavigation)
                .FirstOrDefaultAsync(e => e.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User
                    .FindFirst(ClaimTypes.Name).Value);

            return meeting == null ? NotFound() : View(meeting);
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