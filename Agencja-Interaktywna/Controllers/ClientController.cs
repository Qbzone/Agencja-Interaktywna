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
    [Authorize(Roles = "Klient")]
    public class ClientController : Controller
    {
        private readonly Models.InteractiveAgencyContext _s16693context = new Models.InteractiveAgencyContext();
        public async Task<IActionResult> Index()
        {
            ViewBag.userEmail = HttpContext.User.Identity.Name;

            var pr = await _s16693context.Project
                .Include(f => f.IdFirmaNavigation)
                    .ThenInclude(kf => kf.ClientCompany)
                        .ThenInclude(k => k.ClientIdNavigation)
                            .ThenInclude(o => o.ClientIdNavigation)
                .Where(x => x.IdFirmaNavigation.ClientCompany.Any(e => e.ClientIdNavigation.ClientIdNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value))
                .ToListAsync();
            
            var kf = await _s16693context.ClientCompany
                .Include(k => k.ClientIdNavigation)
                    .ThenInclude(o => o.ClientIdNavigation)
                .FirstOrDefaultAsync(e => e.ClientIdNavigation.ClientIdNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);

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
            var kl = await _s16693context.Client
                .Include(o => o.ClientIdNavigation)
                    .FirstOrDefaultAsync(e => e.ClientIdNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);
            
            var aCM = new AssignCompanyModel
            {
                Klient = kl
            };

            return View(aCM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCompany(AssignCompanyModel aCM)
        {
            if (ModelState.IsValid)
            {
                var newFirma = new Company()
                {
                    CompanyId = aCM.Firma.CompanyId,
                    CompanyName = aCM.Firma.CompanyName
                };

                _s16693context.Add(newFirma);
                await _s16693context.SaveChangesAsync();

                var newKF = new ClientCompany()
                {
                    ClientId = aCM.Klient.ClientId,
                    CompanyId = newFirma.CompanyId
                };

                _s16693context.Add(newKF);

                await _s16693context.SaveChangesAsync();
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
            var meet = await _s16693context.EmployeeClient
                .Include(k => k.ClientIdNavigation)
                    .ThenInclude(o => o.ClientIdNavigation)
                .Include(p => p.EmployeeIdNavigation)
                    .ThenInclude(po => po.EmployeeIdNavigation)
                .Where(x => x.ClientIdNavigation.ClientIdNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                .ToListAsync();
            return View(meet);
        }

        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _s16693context.Project
            .FirstOrDefaultAsync(e => e.IdProjekt == id);

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
            var team = await _s16693context.TeamProject
                .Include(p => p.IdProjektNavigation)
                .Include(z => z.IdZespolNavigation)
                .FirstOrDefaultAsync(x => x.IdProjekt == id && x.DataWypisaniaZespolu == null);

            var members = await _s16693context.EmployeeTeam
                .Include(z => z.TeamIdNavigation)
                .Include(p => p.EmployeeIdNavigation)
                    .ThenInclude(o => o.EmployeeIdNavigation)
                    .Where(x => x.TeamId == team.IdZespol)
                    .ToListAsync();

            return View(members);

        }

        public async Task<IActionResult> Contract(int? id)
        {
            var contract = await _s16693context.ProjectPackage
                .Include(pr => pr.IdProjektNavigation)
                .Include(pa => pa.IdPakietNavigation)
                .Where(x => x.IdProjekt == id && x.DataZakonczeniaWspolpracy == null)
                .OrderByDescending(e => e.IdPakiet)
                .ToListAsync();

            return View(contract);
        }

        public async Task<IActionResult> Profile()
        {
            var kl = await _s16693context.Client
                .Include(o => o.ClientIdNavigation)
                .FirstOrDefaultAsync(i => i.ClientIdNavigation.AdresEmail == HttpContext.User.FindFirst(ClaimTypes.Name).Value);
           
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