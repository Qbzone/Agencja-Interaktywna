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
                }

                osoba.KodAktywacyjny = Guid.NewGuid();
                osoba.Haslo = Hash(osoba.Haslo);
                osoba.CzyEmailZweryfikowane = false;

                s16693Context context2 = new s16693Context();
                {
                    context2.Osoba.Add(osoba);
                    context2.SaveChanges();

                    SendVerificationLink(osoba);
                    Message = "Registration succesfully done. Account activation link has been sent to your email: " + osoba.AdresEmail;
                    Status = true;
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

        public IActionResult Login()
        {
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

        [NonAction]
        public void SendVerificationLink(Osoba osoba)
        {
            var fromEmail = new MailAddress("johnytestin@gmail.com");
            var toEmail = new MailAddress(osoba.AdresEmail);
            var fromEmaiilPassword = "123qwER#$";
            string confirmationLink = Url.Action("ConfirmEmail", "Account", new { id = osoba.Idosoba, token = osoba.KodAktywacyjny });
            string subject = "Twoje je konto jest w pełni utworzone";
            string body = "<br/><br/>We are excited to tell you that your account is" +
                " succesfully created. Please click on the lint to verify your account" +
                " <br/><br/><a href='" + confirmationLink + "'>" + confirmationLink + "</a>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmaiilPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }) smtp.Send(message);

            
        }

        public static string Hash(string Value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Value)));
        }

    }
}
