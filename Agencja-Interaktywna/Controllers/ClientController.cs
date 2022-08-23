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

            var pr = await _interactiveAgencyContext.Project
                .Include(f => f.CompanyIdNavigation)
                    .ThenInclude(kf => kf.ClientCompany)
                        .ThenInclude(k => k.ClientIdNavigation)
                            .ThenInclude(o => o.ClientIdNavigation)
                .Where(x => x.CompanyIdNavigation.ClientCompany.Any(e => e.ClientIdNavigation.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value))
                .ToListAsync();
            
            var kf = await _interactiveAgencyContext.ClientCompany
                .Include(k => k.ClientIdNavigation)
                    .ThenInclude(o => o.ClientIdNavigation)
                .FirstOrDefaultAsync(e => e.ClientIdNavigation.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

            if (kf != null)
            {
                return View(pr);
            }
            else
            {
                return RedirectToAction(nameof(AssignCompany));
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> AssignCompany()
        {
            var kl = await _interactiveAgencyContext.Client
                .Include(o => o.ClientIdNavigation)
                    .FirstOrDefaultAsync(e => e.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);
            
            var aCM = new AssignCompanyModel
            {
                Client = kl
            };

            return View(aCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCompany(AssignCompanyModel aCM)
        {
            if (ModelState.IsValid)
            {
                var newCompany = new Company()
                {
                    CompanyId = aCM.Company.CompanyId,
                    CompanyName = aCM.Company.CompanyName
                };

                _interactiveAgencyContext.Add(newCompany);
                await _interactiveAgencyContext.SaveChangesAsync();

                var newKF = new ClientCompany()
                {
                    ClientId = aCM.Client.ClientId,
                    CompanyId = newCompany.CompanyId
                };

                _interactiveAgencyContext.Add(newKF);

                await _interactiveAgencyContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                return View("AssignCompany", aCM);
            }
            return View(aCM);
        }

        public async Task<IActionResult> Meetings()
        {
            var meet = await _interactiveAgencyContext.EmployeeClient
                .Include(k => k.ClientIdNavigation)
                    .ThenInclude(o => o.ClientIdNavigation)
                .Include(p => p.EmployeeIdNavigation)
                    .ThenInclude(po => po.EmployeeIdNavigation)
                .Where(x => x.ClientIdNavigation.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                .ToListAsync();
            return View(meet);
        }

        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _interactiveAgencyContext.Project
            .FirstOrDefaultAsync(e => e.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return View(project);
            }


        }

        public async Task<IActionResult> Team(int? id)
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

        public async Task<IActionResult> Contract(int? id)
        {
            var contract = await _interactiveAgencyContext.ProjectPackage
                .Include(pr => pr.ProjectIdNavigation)
                .Include(pa => pa.PackageIdNavigation)
                .Where(x => x.ProjectId == id && x.DealEnd == null)
                .OrderByDescending(e => e.PackageId)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Profile()
        {
            var kl = await _interactiveAgencyContext.Client
                .Include(o => o.ClientIdNavigation)
                .FirstOrDefaultAsync(i => i.ClientIdNavigation.EmailAddress == HttpContext.User.FindFirst(ClaimTypes.Name).Value);
           
            if(kl == null)
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