using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Interactive_Agency.Models;
using System.Net.Mail;
using System.Net;
using Interactive_Agency.Models.Functional;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Interactive_Agency.Controllers
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
        public async Task<IActionResult> Register(Person person)
        {
            bool Status = false;
            string Message = "";

            ModelState.Remove(nameof(Person.IsEmailVerified));
            ModelState.Remove(nameof(Person.ActivationCode));

            if (ModelState.IsValid)
            {
                InteractiveAgencyContext context1 = new InteractiveAgencyContext();
                {
                    var check = await context1.Person.Where(e => e.EmailAddress == person.EmailAddress).FirstOrDefaultAsync();

                    if (check != null)
                    {
                        ModelState.AddModelError("EmailAddress", "Podany adres e-mail już istnieje");
                    }
                    else
                    {
                        person.ActivationCode = Guid.NewGuid();
                        person.Password = Hash(person.Password);
                        person.IsEmailVerified = false;
                        person.Role = "Client";

                        InteractiveAgencyContext context2 = new InteractiveAgencyContext();
                        {
                            context2.Person.Add(person);
                            await context2.SaveChangesAsync();

                            SendVerificationLink(person);

                            Message = "Rejestracja zakończona pomyślnie. Link do aktywacji konta został przesłany na twój adres e-mail " + 
                                person.EmailAddress;
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
            
            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> Verify(string id)
        {
            bool Status = false;
            using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
            {
                var v = await dc.Person.Where(e => e.ActivationCode == new Guid(id)).FirstOrDefaultAsync();

                if (v != null)
                {
                    v.IsEmailVerified = true;
                    Client klient = new Client() 
                    {
                        ClientId = v.PersonId,
                        Priority = "no"
                     };

                    dc.Client.Add(klient);
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
            using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
            {
                var v = await dc.Person.Where(e => e.EmailAddress == login.EmailAddress).FirstOrDefaultAsync();
                if (v != null)
                {
                    if (v.IsEmailVerified != false)
                    {
                        if (Hash(login.Password) == v.Password)
                        {
                            ClaimsIdentity identity = null;
                            bool isAutheticate = false;
                            if (v.Role == "Klient")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.EmailAddress),
                                        new Claim(ClaimTypes.Role, v.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Klient");
                            }
                            else if (v.Role == "Szef")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.EmailAddress),
                                        new Claim(ClaimTypes.Role, v.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Szef");
                            }
                            else if (v.Role == "Programista")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.EmailAddress),
                                        new Claim(ClaimTypes.Role, v.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Programista");
                            }
                            else if (v.Role == "Grafik")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.EmailAddress),
                                        new Claim(ClaimTypes.Role, v.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Grafik");
                            }
                            else if (v.Role == "Pozycjoner")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.EmailAddress),
                                        new Claim(ClaimTypes.Role, v.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;
                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }
                                return base.RedirectToAction("Index", "Pozycjoner");
                            }
                            else if (v.Role == "Tester")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, v.EmailAddress),
                                        new Claim(ClaimTypes.Role, v.Role)
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

            using (Models.InteractiveAgencyContext dc = new Models.InteractiveAgencyContext())
            {
                var v = await dc.Person.Where(e => e.EmailAddress == osoba.EmailAddress).FirstOrDefaultAsync();

                if (v != null)
                {
                    SendPasswordReset(v);
                    Message = "Link do zmiany hasła został przesłany na twój adres e-mail " + osoba.EmailAddress;
                    Status = true;
                }
                else
                {
                    ModelState.AddModelError("EmailAddress", "Podany adres e-mail nie istnieje");
                }
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View(osoba);
        }

        [HttpGet]
        public async Task<IActionResult> ForgottenPassword(string id)
        {
            using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
            {
                var v = await dc.Person.Where(e => e.ActivationCode == new Guid(id)).FirstOrDefaultAsync();

                if (v != null)
                {
                    PersonForgottenPassword oFP = new PersonForgottenPassword();
                    oFP.EmailAddress = v.EmailAddress;
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
                using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
                {
                    var v = await dc.Person.Where(e => e.EmailAddress == oFP.EmailAddress).FirstOrDefaultAsync();

                    if (v != null)
                    {
                        oFP.Password = Hash(oFP.Password);
                        v.Password = oFP.Password;
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

            string confirmationLink = Request.Scheme + "://" + Request.Host + "/Home/Verify/" + osoba.ActivationCode;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(osoba.EmailAddress);
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

            string resetLink = Request.Scheme + "://" + Request.Host + "/Home/ForgottenPassword/" + osoba.ActivationCode;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(osoba.EmailAddress);
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
