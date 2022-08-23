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
                        ModelState.AddModelError("EmailAddress", "The email address you provided already exists.");
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

                            Message = "Registration successfully completed. The link to activate your account has been sent to your email address " +
                                person.EmailAddress + ".";
                            Status = true;
                        }
                    }
                }
            }
            else
            {
                Message = "Incorrect request.";
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
                    ViewBag.Message = "Incorrect request.";
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
        public async Task<IActionResult> Login(PersonLogin personLogin)
        {
            using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
            {
                var v = await dc.Person.Where(e => e.EmailAddress == personLogin.EmailAddress).FirstOrDefaultAsync();
                if (v != null)
                {
                    if (v.IsEmailVerified != false)
                    {
                        if (Hash(personLogin.Password) == v.Password)
                        {
                            ClaimsIdentity identity = null;
                            bool isAutheticate = false;

                            if (v.Role == "Client")
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
                                return base.RedirectToAction("Index", "Client");
                            }
                            else if (v.Role == "Boss")
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
                                return base.RedirectToAction("Index", "Boss");
                            }
                            else if (v.Role == "Programmer")
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
                                return base.RedirectToAction("Index", "Programmer");
                            }
                            else if (v.Role == "Graphician")
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
                                return base.RedirectToAction("Index", "Graphician");
                            }
                            else if (v.Role == "Positioner")
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
                                return base.RedirectToAction("Index", "Positioner");
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
                            ModelState.AddModelError("Password", "The password you entered is incorrect.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "The e-mail address provided has not yet been verified.");
                    }
                }
                else
                {
                    ModelState.AddModelError("EmailAddress", "The e-mail address provided does not exist.");
                }
            }
            return View(personLogin);
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
        public async Task<IActionResult> Token(PersonToken personToken)
        {
            bool Status = false;
            string Message = "";

            using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
            {
                var v = await dc.Person.Where(e => e.EmailAddress == personToken.EmailAddress).FirstOrDefaultAsync();

                if (v != null)
                {
                    SendPasswordReset(v);
                    Message = "The link to change your password was sent to your email address " + personToken.EmailAddress + ".";
                    Status = true;
                }
                else
                {
                    ModelState.AddModelError("EmailAddress", "The email address you provided does not exist.");
                }
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View(personToken);
        }

        [HttpGet]
        public async Task<IActionResult> ForgottenPassword(string id)
        {
            using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
            {
                var v = await dc.Person.Where(e => e.ActivationCode == new Guid(id)).FirstOrDefaultAsync();

                if (v != null)
                {
                    PersonForgottenPassword pFP = new PersonForgottenPassword();
                    pFP.EmailAddress = v.EmailAddress;
                    return base.View(pFP);
                }
                else
                {
                    return base.RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgottenPassword(PersonForgottenPassword pFP)
        {
            var Message = "";

            if (ModelState.IsValid)
            {
                using (InteractiveAgencyContext dc = new InteractiveAgencyContext())
                {
                    var v = await dc.Person.Where(e => e.EmailAddress == pFP.EmailAddress).FirstOrDefaultAsync();

                    if (v != null)
                    {
                        pFP.Password = Hash(pFP.Password);
                        v.Password = pFP.Password;
                        dc.Update<Person>(v);
                        dc.SaveChanges();
                        Message = "The password has been updated successfully.";
                        return base.RedirectToAction("Login", "Home");
                    }
                }
            }
            else
            {
                Message = "Incorrect request.";
            }
            ViewBag.Message = Message;

            return View(pFP);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SendVerificationLink(Person person)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            string confirmationLink = Request.Scheme + "://" + Request.Host + "/Home/Verify/" + person.ActivationCode;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(person.EmailAddress);
            mail.Subject = "Your account is fully established.";
            mail.Body = "<br/><br/>We are proud to announce that your account has been successfully created. " + "" +
                "Please follow the link we sent you to activate your account. <br/><br/><a href='" +
                confirmationLink + "'>" + confirmationLink + "</a>";
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential("johnytestin@gmail.com", "adgqnuoumkbqfecg");

            SmtpServer.Send(mail);
        }

        public void SendPasswordReset(Person person)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            string resetLink = Request.Scheme + "://" + Request.Host + "/Home/ForgottenPassword/" + person.ActivationCode;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(person.EmailAddress);
            mail.Subject = "Password reset.";
            mail.Body = "<br/><br/>A request to change the password was made to this e-mail address. " + "" +
                "To change the password assigned to your account, follow this link. <br/><br/><a href='" +
                resetLink + "'>" + resetLink + "</a>";
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("johnytestin@gmail.com", "adgqnuoumkbqfecg");

            SmtpServer.Send(mail);
        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}