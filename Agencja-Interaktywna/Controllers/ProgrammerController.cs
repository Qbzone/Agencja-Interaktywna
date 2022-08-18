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
    [Authorize(Roles = "Programista")]
    public class ProgrammerController : Controller
    {
        private readonly InteractiveAgencyContext _s16693context = new InteractiveAgencyContext();
        public async Task<IActionResult> Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var zespol = await _s16693context.Team
                    .Include(pz => pz.EmployeeTeam)
                        .ThenInclude(p => p.EmployeeIdNavigation)
                            .ThenInclude(o => o.EmployeeIdNavigation)
                    .Where(x => x.EmployeeTeam.Any(e => e.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value)).ToListAsync();
            
            List<Project> projekts = new List<Project>();
   
            foreach(var item in zespol)
            {

                projekts.AddRange(await _s16693context.Project
                .Include(zp => zp.TeamProject)
                    .ThenInclude(z => z.TeamIdNavigation)
                .Where(x => x.TeamProject.Any(e => e.TeamId == item.TeamId && e.AssignEnd == null)).ToListAsync());
            }

            return View(projekts);
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
                .Where(e => e.ProjectId == id && e.ServiceIdNavigation.Classification == HttpContext.User.FindFirst(ClaimTypes.Role).Value)
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
        public async Task<IActionResult> Teams()
        {
            var teams = await _s16693context.Team
                    .Include(pz => pz.EmployeeTeam)
                        .ThenInclude(p => p.EmployeeIdNavigation)
                            .ThenInclude(o => o.EmployeeIdNavigation)
                    .Where(x => x.EmployeeTeam.Any(e => e.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value)).ToListAsync();

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
        public async Task<IActionResult> TaskDelay(TaskEditModel tEM)
        {
            if (ModelState.IsValid)
            {
                DateTime tmpDate = (DateTime)tEM.ServiceProject.AssignEnd;
                tEM.ServiceProject.AssignEnd = tmpDate.AddDays(7);
                tEM.ServiceProject.Status = "Opóźnione";
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

        public async Task<IActionResult> Meetings()
        {
            var meetings = await _s16693context.EmployeeClient
                .Include(k => k.ClientIdNavigation)
                    .ThenInclude(o => o.ClientIdNavigation)
                .Include(p => p.EmployeeIdNavigation)
                    .ThenInclude(po => po.EmployeeIdNavigation)
                .Where(e => e.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value).ToListAsync();

            return View(meetings);
        }



        public async Task<IActionResult> Profile()
        {
            var kl = await _s16693context.EmployeeContract
                .Include(p => p.EmployeeIdNavigation)
                    .ThenInclude(o => o.EmployeeIdNavigation)
                .Include(u => u.ContractIdNavigation)
                .FirstOrDefaultAsync(i => i.EmployeeIdNavigation.EmployeeIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

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