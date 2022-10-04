using System;
using System.Linq;
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

namespace Agencja_Interaktywna.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly InteractiveAgencyContext _interactiveAgencyContext = new InteractiveAgencyContext();
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public AdminController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            return View();
        }

        public async Task<IActionResult> Companies()
        {
            return View();
        }

        public async Task<IActionResult> Contracts()
        {
            return View();
        }

        public async Task<IActionResult> Packages()
        {
            return View();
        }

        public async Task<IActionResult> Persons()
        {
            return View();
        }

        public async Task<IActionResult> ProgrammingLanguages()
        {
            return View();
        }

        public async Task<IActionResult> Projects()
        {
            return View();
        }

        public async Task<IActionResult> Services()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            var teams = await _interactiveAgencyContext.Team.ToListAsync();

            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> TeamDetails(int? teamId)
        {
            var members = await _interactiveAgencyContext.EmployeeTeam
                .Include(te => te.TeamIdNavigation)
                .Include(em => em.EmployeeIdNavigation)
                    .ThenInclude(emp => emp.EmployeeIdNavigation)
                    .Where(e => e.TeamId == teamId)
                    .ToListAsync();

            return View(members);
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}