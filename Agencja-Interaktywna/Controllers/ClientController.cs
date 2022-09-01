using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Interactive_Agency.Models;
using Interactive_Agency.Models.Functional;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interactive_Agency.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly InteractiveAgencyContext _interactiveAgencyContext = new InteractiveAgencyContext();

        public async Task<IActionResult> Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var projects = await _interactiveAgencyContext.Project
                .Include(co => co.CompanyIdNavigation)
                    .ThenInclude(cc => cc.ClientCompany)
                        .ThenInclude(cl => cl.ClientIdNavigation)
                            .ThenInclude(cli => cli.ClientIdNavigation)
                .Where(e => e.CompanyIdNavigation.ClientCompany
                    .Any(f => f.ClientIdNavigation.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value))
                .ToListAsync();
            var clientCompany = await _interactiveAgencyContext.ClientCompany
                .Include(cl => cl.ClientIdNavigation)
                    .ThenInclude(cli => cli.ClientIdNavigation)
                .FirstOrDefaultAsync(e => e.ClientIdNavigation.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            return clientCompany != null ? View(projects) : RedirectToAction(nameof(AssignCompany));
        }

        [HttpGet]
        public async Task<IActionResult> AssignCompany()
        {
            var client = await _interactiveAgencyContext.Client
                .Include(o => o.ClientIdNavigation)
                    .FirstOrDefaultAsync(e => e.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            var assignCompany = new AssignCompanyModel
            {
                Client = client
            };

            return View(assignCompany);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCompany(AssignCompanyModel assignCompany)
        {
            if (ModelState.IsValid)
            {
                var newCompany = new Company()
                {
                    CompanyId = assignCompany.Company.CompanyId,
                    CompanyName = assignCompany.Company.CompanyName
                };

                _interactiveAgencyContext.Add(newCompany);
                await _interactiveAgencyContext.SaveChangesAsync();

                var newClientCompany = new ClientCompany()
                {
                    ClientId = assignCompany.Client.ClientId,
                    CompanyId = newCompany.CompanyId
                };

                _interactiveAgencyContext.Add(newClientCompany);
                await _interactiveAgencyContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                return View("AssignCompany", assignCompany);
            }

            return View(assignCompany);
        }

        public async Task<IActionResult> Meetings()
        {
            var employeeClients = await _interactiveAgencyContext.EmployeeClient
                .Include(cl => cl.ClientIdNavigation)
                    .ThenInclude(cli => cli.ClientIdNavigation)
                .Include(em => em.EmployeeIdNavigation)
                    .ThenInclude(emp => emp.EmployeeIdNavigation)
                .Where(e => e.ClientIdNavigation.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                .ToListAsync();

            return View(employeeClients);
        }

        public async Task<IActionResult> ProjectDetails(int? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var project = await _interactiveAgencyContext.Project
                .FirstOrDefaultAsync(e => e.ProjectId == projectId);

            return project == null ? NotFound() : View(project);
        }

        public async Task<IActionResult> Team(int? teamId)
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

        public async Task<IActionResult> Contract(int? projectId)
        {
            var contract = await _interactiveAgencyContext.ProjectPackage
                .Include(pr => pr.ProjectIdNavigation)
                .Include(pa => pa.PackageIdNavigation)
                .Where(e => e.ProjectId == projectId && e.DealEnd == null)
                .OrderByDescending(e => e.PackageId)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Profile()
        {
            var client = await _interactiveAgencyContext.Client
                .Include(cl => cl.ClientIdNavigation)
                .FirstOrDefaultAsync(e => e.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            return client == null ? NotFound() : View(client);
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