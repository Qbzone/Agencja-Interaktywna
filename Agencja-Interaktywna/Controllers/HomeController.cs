using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Agencja_Interaktywna.Models;
using System.Net.Mail;
using System.Net;
using Agencja_Interaktywna.Models.Functional;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Agencja_Interaktywna.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Person osoba)
        {
            bool Status = false;
            string Message = "";

            ModelState.Remove(nameof(Person.CzyEmailZweryfikowany));
            ModelState.Remove(nameof(Person.KodAktywacyjny));

            if (ModelState.IsValid)
            {
                Models.DbContext context1 = new Models.DbContext();
                {
                    var check = await context1.Osoba.Where(e => e.AdresEmail == osoba.AdresEmail).FirstOrDefaultAsync();

                    if (check != null)
                    {
                        ModelState.AddModelError("AdresEmail", "Podany adres e-mail już istnieje");
                    }
                    else
                    {
                        osoba.KodAktywacyjny = Guid.NewGuid();
                        osoba.Haslo = Hash(osoba.Haslo);
                        osoba.CzyEmailZweryfikowany = false;
                        osoba.Rola = "Klient";

                        Models.DbContext context2 = new Models.DbContext();
                        {
                            context2.Osoba.Add(osoba);
                            await context2.SaveChangesAsync();

                            SendVerificationLink(osoba);

                            Message = "Rejestracja zakończona pomyślnie. Link do aktywacji konta został przesłany na twój adres e-mail " + osoba.AdresEmail;
                            Status = true;
                        }
                    }
                }
            }
            else
            {
                Message = "Nieprawidłowe żądanie";
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(osoba);
        }

        [HttpGet]
        public async Task<IActionResult> Verify(string id)
        {
            bool Status = false;
            using (Models.DbContext dc = new Models.DbContext())
            {
                var v = await dc.Osoba.Where(e => e.KodAktywacyjny == new Guid(id)).FirstOrDefaultAsync();

                if (v != null)
                {
                    v.CzyEmailZweryfikowany = true;
                    Client klient = new Client() 
                    {
                        IdKlient = v.IdOsoba,
                        Priorytet = "nie"
                     };

                    dc.Klient.Add(klient);
                    await dc.SaveChangesAsync();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Nieprawidłowe żądanie";
                }
            }
            ViewBag.Status = Status;

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(PersonLogin login)
        {
            using (Models.DbContext dc = new Models.DbContext())
            {
                var v = await dc.Osoba.Where(e => e.AdresEmail == login.AdresEmail).FirstOrDefaultAsync();
                if (v != null)
                {
                    if (v.CzyEmailZweryfikowany != false)
                    {
                        if (Hash(login.Haslo) == v.Haslo)
                        {
                            ClaimsIdentity identity = null;
                            bool isAutheticate = false;
                            if (v.Rola == "Klient")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.AdresEmail),
                                        new Claim(ClaimTypes.Role, v.Rola)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Klient");
                            }
                            else if (v.Rola == "Szef")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.AdresEmail),
                                        new Claim(ClaimTypes.Role, v.Rola)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Szef");
                            }
                            else if (v.Rola == "Programista")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.AdresEmail),
                                        new Claim(ClaimTypes.Role, v.Rola)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Programista");
                            }
                            else if (v.Rola == "Grafik")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.AdresEmail),
                                        new Claim(ClaimTypes.Role, v.Rola)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Grafik");
                            }
                            else if (v.Rola == "Pozycjoner")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.AdresEmail),
                                        new Claim(ClaimTypes.Role, v.Rola)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Pozycjoner");
                            }
                            else if (v.Rola == "Tester")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.AdresEmail),
                                        new Claim(ClaimTypes.Role, v.Rola)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Tester");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("Haslo", "Podane hasło jest niepoprawne");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("AdresEmail", "Podany adres e-mail nie został jeszcze zweryfikowany");
                    }
                }
                else
                {
                    ModelState.AddModelError("AdresEmail", "Podany adres e-mail nie istnieje");
                }
            }
            return View(login);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Token()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Token(PersonToken osoba)
        {
            bool Status = false;
            string Message = "";

            using (Models.DbContext dc = new Models.DbContext())
            {
                var v = await dc.Osoba.Where(e => e.AdresEmail == osoba.AdresEmail).FirstOrDefaultAsync();

                if (v != null)
                {
                    SendPasswordReset(v);
                    Message = "Link do zmiany hasła został przesłany na twój adres e-mail " + osoba.AdresEmail;
                    Status = true;
                }
                else
                {
                    ModelState.AddModelError("AdresEmail", "Podany adres e-mail nie istnieje");
                }
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View(osoba);
        }

        [HttpGet]
        public async Task<IActionResult> ForgottenPassword(string id)
        {
            using (Models.DbContext dc = new Models.DbContext())
            {
                var v = await dc.Osoba.Where(e => e.KodAktywacyjny == new Guid(id)).FirstOrDefaultAsync();

                if (v != null)
                {
                    PersonForgottenPassword oFP = new PersonForgottenPassword();
                    oFP.AdresEmail = v.AdresEmail;
                    return base.View(oFP);
                }
                else
                {
                    return base.RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgottenPassword(PersonForgottenPassword oFP)
        {
            var Message = "";

            if (ModelState.IsValid)
            {
                using (Models.DbContext dc = new Models.DbContext())
                {
                    var v = await dc.Osoba.Where(e => e.AdresEmail == oFP.AdresEmail).FirstOrDefaultAsync();

                    if (v != null)
                    {
                        oFP.Haslo = Hash(oFP.Haslo);
                        v.Haslo = oFP.Haslo;
                        dc.Update<Person>(v);
                        dc.SaveChanges();
                        Message = "Hasło zostało zaktualizowane pomyślnie";
                        return base.RedirectToAction("Login", "Home");
                    }
                }
            }
            else
            {
                Message = "Nieprawidłowe żądanie";
            }
            ViewBag.Message = Message;

            return View(oFP);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SendVerificationLink(Person osoba)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            string confirmationLink = Request.Scheme + "://" + Request.Host + "/Home/Verify/" + osoba.KodAktywacyjny;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(osoba.AdresEmail);
            mail.Subject = "Twoje konto jest w pełni utworzone";
            mail.Body = "<br/><br/>Z dumą informujemy, iż twoje konto zostało pomyślnie utworzone. " + "" +
                "Prosimy o wejście w wysłany przez nas link w celu aktywacji twojego konta. <br/><br/><a href='" +
                confirmationLink + "'>" + confirmationLink + "</a>";
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential("johnytestin@gmail.com", "adgqnuoumkbqfecg");

            SmtpServer.Send(mail);
        }

        public void SendPasswordReset(Person osoba)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            string resetLink = Request.Scheme + "://" + Request.Host + "/Home/ForgottenPassword/" + osoba.KodAktywacyjny;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(osoba.AdresEmail);
            mail.Subject = "Resetowanie hasła";
            mail.Body = "<br/><br/>Na ten adres e-mail złożono prośbę o zmianę hasła. " + "" +
                "W celu zmiany hasła przypisanego do konta należy wejść w ten link. <br/><br/><a href='" +
                resetLink + "'>" + resetLink + "</a>";
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("johnytestin@gmail.com", "adgqnuoumkbqfecg");

            SmtpServer.Send(mail);
        }

        public static string Hash(string Value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Value)));
        }

    }
}
