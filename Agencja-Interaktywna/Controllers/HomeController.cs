﻿using System;
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
using System.Web.Helpers;

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
            s16693Context context = new s16693Context();

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
                Message = "Invalid Request";
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(osoba);
        }

        [HttpGet]
        public ActionResult Verify(String id)
        {
            bool Status = false;
            using (s16693Context dc = new s16693Context())
            {
                var v = dc.Osoba.Where(e => e.KodAktywacyjny == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    v.CzyEmailZweryfikowane = true;
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
        [ValidateAntiForgeryToken]
        public IActionResult Login(OsobaLogin login)
        {
            string message = "";
            using (s16693Context dc = new s16693Context())
            {
                var v = dc.Osoba.Where(e => e.AdresEmail == login.AdresEmail).FirstOrDefault();
                if(v != null)
                {
                    if(string.Compare(Crypto.Hash(login.Haslo), v.Haslo) == 0)
                    {
                        var ticket = new FormsAuthenticationTicket();

                    }
                    else
                    {
                        message = "Podano błędne dane";
                    }
                }
                else
                {
                    message = "Podano błędne dane";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult ForgottenPassword()
        {
            return View();
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

        public static string Hash(string Value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Value)));
        }

    }
}
