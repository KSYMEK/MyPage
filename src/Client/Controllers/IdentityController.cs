namespace Client.Controllers {
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("[controller]/[action]")]
    public class IdentityController : Controller {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger, SignInManager<IdentityUser> signInManager) {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("Logowanie")]
        public IActionResult Login() {
            return View("Login");
        }

        [HttpGet]
        [ActionName("Rejestracja")]
        public IActionResult Register() {
            return View("Register");
        }
    }
}