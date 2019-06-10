namespace Client.Controllers {
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;

    [Route("[action]")]
    public class HomeController : Controller {
        private readonly IOptions<EmailSecrets> _emailSecrets;

        public HomeController(IOptions<EmailSecrets> emailSecrets) {
            _emailSecrets = emailSecrets;
        }

        [HttpGet("/")]
        public IActionResult Index() {
            return View();
        }


        [HttpGet("/kontakt")]
        public IActionResult Contact() {
            return View();
        }

        [HttpGet("/kontakt/powiadomienie")]
        public IActionResult MessageResponse([FromQuery(Name = "Powodzenie")] bool? isSent = null) {
            if (isSent == null) return RedirectToAction("Contact");

            ViewData["IsSent"] = isSent;
            return View();
        }

        [HttpGet("/o-mnie")]
        public IActionResult About() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactModel model) {
            var smtpClient = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSecrets.Value.Login, _emailSecrets.Value.Password)
            };

            try {
                using (var message = new MailMessage(_emailSecrets.Value.Login, "kacper.szymczuch@gmail.com") {
                    Subject = model.Topic + " - od " + model.Sender,
                    Body = model.Message
                }) {
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception) {
                return RedirectToAction("MessageResponse", new {Powodzenie = true});
            }

            return RedirectToAction("MessageResponse", new {Powodzenie = true});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}