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
                InteractiveAgencyContext firstIAContext = new InteractiveAgencyContext();
                {
                    var check = await firstIAContext.Person.Where(e => e.EmailAddress == person.EmailAddress).FirstOrDefaultAsync();

                    if (check != null)
                    {
                        ModelState.AddModelError("EmailAddress", "Provided e-mail address alread exists.");
                    }
                    else
                    {
                        person.ActivationCode = Guid.NewGuid();
                        person.Password = Hash(person.Password);
                        person.IsEmailVerified = false;
                        person.Role = "Client";

                        InteractiveAgencyContext secondIAContext = new InteractiveAgencyContext();
                        {
                            secondIAContext.Person.Add(person);
                            await secondIAContext.SaveChangesAsync();
                            SendVerificationLink(person);

                            Message = "Registration successfully completed. Activation link has been sent to your email address " +
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
        public async Task<IActionResult> Verify(string personId)
        {
            bool Status = false;

            InteractiveAgencyContext iAContext = new InteractiveAgencyContext();
            {
                var verify = await iAContext.Person.Where(e => e.ActivationCode == new Guid(personId)).FirstOrDefaultAsync();

                if (verify != null)
                {
                    verify.IsEmailVerified = true;
                    Client klient = new Client()
                    {
                        ClientId = verify.PersonId,
                        Priority = "No"
                    };

                    iAContext.Client.Add(klient);
                    await iAContext.SaveChangesAsync();

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
            InteractiveAgencyContext iAContext = new InteractiveAgencyContext();
            {
                Person verified = await iAContext.Person.Where(e => e.EmailAddress == personLogin.EmailAddress).FirstOrDefaultAsync();

                if (verified != null)
                {
                    if (verified.IsEmailVerified != false)
                    {
                        if (Hash(personLogin.Password) == verified.Password)
                        {
                            ClaimsIdentity identity = null;
                            bool isAutheticate = false;

                            if (verified.Role == "Client")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, verified.EmailAddress),
                                        new Claim(ClaimTypes.Role, verified.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;

                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }

                                return base.RedirectToAction("Index", "Client");
                            }
                            else if (verified.Role == "Boss")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, verified.EmailAddress),
                                        new Claim(ClaimTypes.Role, verified.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;

                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }

                                return base.RedirectToAction("Index", "Boss");
                            }
                            else if (verified.Role == "Programmer")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, verified.EmailAddress),
                                        new Claim(ClaimTypes.Role, verified.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;

                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }

                                return base.RedirectToAction("Index", "Programmer");
                            }
                            else if (verified.Role == "Graphician")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, verified.EmailAddress),
                                        new Claim(ClaimTypes.Role, verified.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;

                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }

                                return base.RedirectToAction("Index", "Graphician");
                            }
                            else if (verified.Role == "Positioner")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, verified.EmailAddress),
                                        new Claim(ClaimTypes.Role, verified.Role)
                                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                                isAutheticate = true;

                                if (isAutheticate)
                                {
                                    var principal = new ClaimsPrincipal(identity);
                                    var log = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                }

                                return base.RedirectToAction("Index", "Positioner");
                            }
                            else if (verified.Role == "Tester")
                            {
                                identity = new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimTypes.Name, verified.EmailAddress),
                                        new Claim(ClaimTypes.Role, verified.Role)
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
                            ModelState.AddModelError("Password", "Password is incorrect.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "E-mail address has not been verified.");
                    }
                }
                else
                {
                    ModelState.AddModelError("EmailAddress", "E-mail address doesn't exists.");
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

            InteractiveAgencyContext iAContext = new InteractiveAgencyContext();
            {
                var token = await iAContext.Person.Where(e => e.EmailAddress == personToken.EmailAddress).FirstOrDefaultAsync();

                if (token != null)
                {
                    SendPasswordReset(token);

                    Message = "Link to change your password was sent to your email address " + personToken.EmailAddress + ".";
                    Status = true;
                }
                else
                {
                    ModelState.AddModelError("EmailAddress", "E-mail address doesn't exists.");
                }
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View(personToken);
        }

        [HttpGet]
        public async Task<IActionResult> ForgottenPassword(string persondId)
        {
            InteractiveAgencyContext iAContext = new InteractiveAgencyContext();
            {
                var forgotten = await iAContext.Person.Where(e => e.ActivationCode == new Guid(persondId)).FirstOrDefaultAsync();

                if (forgotten != null)
                {
                    PersonForgottenPassword pFPassword = new PersonForgottenPassword();
                    pFPassword.EmailAddress = forgotten.EmailAddress;

                    return base.View(pFPassword);
                }
                else
                {
                    return base.RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgottenPassword(PersonForgottenPassword pFPassword)
        {
            var Message = "";

            if (ModelState.IsValid)
            {
                InteractiveAgencyContext iAContext = new InteractiveAgencyContext();
                {
                    var forgotten = await iAContext.Person.Where(e => e.EmailAddress == pFPassword.EmailAddress).FirstOrDefaultAsync();

                    if (forgotten != null)
                    {
                        pFPassword.Password = Hash(pFPassword.Password);
                        forgotten.Password = pFPassword.Password;

                        iAContext.Update<Person>(forgotten);
                        iAContext.SaveChanges();

                        Message = "Password has been updated successfully.";

                        return base.RedirectToAction("Login", "Home");
                    }
                }
            }
            else
            {
                Message = "Incorrect request.";
            }

            ViewBag.Message = Message;

            return View(pFPassword);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SendVerificationLink(Person person)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            string confirmationLink = Request.Scheme + "://" + Request.Host + "/Home/Verify/" + person.ActivationCode;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(person.EmailAddress);
            mail.Subject = "Your account is fully established.";
            mail.Body = "<br/><br/>We are proud to announce that your account has been successfully created. " + "" +
                "Please follow the link we sent you to activate your account. <br/><br/><a href='" +
                confirmationLink + "'>" + confirmationLink + "</a>";
            mail.IsBodyHtml = true;

            smtpServer.Port = 587;
            smtpServer.EnableSsl = true;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new NetworkCredential("johnytestin@gmail.com", "adgqnuoumkbqfecg");
            smtpServer.Send(mail);
        }

        public void SendPasswordReset(Person person)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            string resetLink = Request.Scheme + "://" + Request.Host + "/Home/ForgottenPassword/" + person.ActivationCode;

            mail.From = new MailAddress("johnytestin@gmail.com");
            mail.To.Add(person.EmailAddress);
            mail.Subject = "Password reset.";
            mail.Body = "<br/><br/>A request to change the password was made to this e-mail address. " + "" +
                "To change the password assigned to your account, follow this link. <br/><br/><a href='" +
                resetLink + "'>" + resetLink + "</a>";
            mail.IsBodyHtml = true;

            smtpServer.Port = 587;
            smtpServer.EnableSsl = true;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new System.Net.NetworkCredential("johnytestin@gmail.com", "adgqnuoumkbqfecg");
            smtpServer.Send(mail);
        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}