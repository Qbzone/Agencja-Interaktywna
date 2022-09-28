using Interactive_Agency.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Teams()
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