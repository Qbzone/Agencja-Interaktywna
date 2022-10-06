﻿using System;
using System.Collections.Generic;
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}