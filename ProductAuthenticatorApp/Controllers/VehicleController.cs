using Microsoft.AspNetCore.Mvc;

namespace ProductAuthenticatorApp.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
