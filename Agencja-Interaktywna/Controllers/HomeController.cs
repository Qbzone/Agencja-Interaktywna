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

namespace Agencja_Interaktywna.Controllers
{
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
        public IActionResult Register(Osoba osoba)
        {
            bool Status = false;
            string Message = "";

            ModelState.Remove(nameof(Osoba.CzyEmailZweryfikowane));
            ModelState.Remove(nameof(Osoba.KodAktywacyjny));

            if (ModelState.IsValid)
            {
                s16693Context context1 = new s16693Context();
                {
                    var check = context1.Osoba.Where(e => e.AdresEmail == osoba.AdresEmail).FirstOrDefault();

                    if (check != null)
                    {
                        ModelState.AddModelError("AdresEmail", "Podany adres e-mail już istnieje");
                    }
                    else
                    {
                        osoba.KodAktywacyjny = Guid.NewGuid();
                        osoba.Haslo = Hash(osoba.Haslo);
                        osoba.CzyEmailZweryfikowane = false;

                        s16693Context context2 = new s16693Context();
                        {
                            context2.Osoba.Add(osoba);
                            context2.SaveChanges();

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
        public ActionResult Verify(string id)
        {
            bool Status = false;
            using (s16693Context dc = new s16693Context())
            {
                var v = dc.Osoba.Where(e => e.KodAktywacyjny == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    v.CzyEmailZweryfikowane = true;
                    Klient klient = new Klient();
                    klient.Idklient = v.Idosoba;
                    klient.Priorytet = "nie";

                    dc.Klient.Add(klient);
                    dc.SaveChanges();
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
        public IActionResult Login(OsobaLogin login)
        {
            using (s16693Context dc = new s16693Context())
            {
                var v = dc.Osoba.Where(e => e.AdresEmail == login.AdresEmail).FirstOrDefault();

                if (v != null)
                {
                    if (v.CzyEmailZweryfikowane != false)
                    {
                        if (Hash(login.Haslo) == v.Haslo)
                        {
                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, login.AdresEmail)
                        };
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var props = new AuthenticationProperties();

                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                            if (v.Idosoba >= 1 && v.Idosoba < 100)
                            {
                                return RedirectToAction("Index", "Szef");
                            }
                            else if (v.Idosoba >= 100 && v.Idosoba < 350)
                            {
                                return RedirectToAction("Index", "Programista");
                            }
                            else if (v.Idosoba >= 350 && v.Idosoba < 600)
                            {
                                return RedirectToAction("Index", "Grafik");
                            }
                            else if (v.Idosoba >= 600 && v.Idosoba < 800)
                            {
                                return RedirectToAction("Index", "Pozycjoner");
                            }
                            else if (v.Idosoba >= 800 && v.Idosoba < 1000)
                            {
                                return RedirectToAction("Index", "Tester");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Klient");
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
        public IActionResult Token(OsobaToken osoba)
        {
            bool Status = false;
            string Message = "";

            using (s16693Context dc = new s16693Context())
            {
                var v = dc.Osoba.Where(e => e.AdresEmail == osoba.AdresEmail).FirstOrDefault();

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
        public IActionResult ForgottenPassword(string id)
        {
            using (s16693Context dc = new s16693Context())
            {
                var v = dc.Osoba.Where(e => e.KodAktywacyjny == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    OsobaForgottenPassword oFP = new OsobaForgottenPassword();
                    oFP.AdresEmail = v.AdresEmail;
                    return View(oFP);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgottenPassword(OsobaForgottenPassword oFP)
        {
            var Message = "";

            if (ModelState.IsValid)
            {
                using (s16693Context dc = new s16693Context())
                {
                    var v = dc.Osoba.Where(e => e.AdresEmail == oFP.AdresEmail).FirstOrDefault();

                    if (v != null)
                    {
                        oFP.Haslo = Hash(oFP.Haslo);
                        v.Haslo = oFP.Haslo;
                        dc.Update(v);
                        dc.SaveChanges();
                        Message = "Hasło zostało zaktualizowane pomyślnie";
                        return RedirectToAction("Login", "Home");
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

        public void SendVerificationLink(Osoba osoba)
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
            SmtpServer.Credentials = new System.Net.NetworkCredential("johnytestin@gmail.com", "123qwER#$");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public void SendPasswordReset(Osoba osoba)
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
            SmtpServer.Credentials = new System.Net.NetworkCredential("johnytestin@gmail.com", "123qwER#$");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public static string Hash(string Value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Value)));
        }

    }
}
