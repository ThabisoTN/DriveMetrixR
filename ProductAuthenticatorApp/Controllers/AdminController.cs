using Microsoft.AspNetCore.Mvc;

namespace ProductAuthenticatorApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

     
    }
}
