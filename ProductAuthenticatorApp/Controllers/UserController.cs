using Microsoft.AspNetCore.Mvc;

namespace ProductAuthenticatorApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult ViewItems() 
        {
            return View();
        }
    }
}
