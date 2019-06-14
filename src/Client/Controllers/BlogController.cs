namespace Client.Controllers {
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]/[action]")]
    public class BlogController : Controller {
        public bool IsMaintenanceBreak { get; set; } = true;

        [HttpGet("/blog")]
        public IActionResult Index() {
            if (IsMaintenanceBreak)
                return View("Maintenance");

            return View();
        }
    }
}