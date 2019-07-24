namespace Client.Controllers {
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Models;

    [Route("[action]")]
    public class HomeController : Controller {
        private readonly EmailSecrets _emailSecrets;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger) {
            _logger = logger;
            _emailSecrets = new EmailSecrets {
                Login = configuration["emailLogin"],
                Password = configuration["emailPassword"]
            };

            _logger.LogWarning($"Logger: {_emailSecrets.Login} {_emailSecrets.Password}");
            Trace.WriteLine($"Trace: {_emailSecrets.Login} {_emailSecrets.Password}");
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

        [HttpGet("/oferta")]
        public IActionResult Offer() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactModel model) {
            var smtpClient = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSecrets.Login, _emailSecrets.Password)
            };

            try {
                using (var message = new MailMessage(_emailSecrets.Login, "kacper.szymczuch@gmail.com") {
                    Subject = model.Topic + " - od " + model.Sender,
                    Body = model.Message
                }) {
                    await smtpClient.SendMailAsync(message);
                    _logger.LogInformation($"Email from {model.Sender} has been send.");
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