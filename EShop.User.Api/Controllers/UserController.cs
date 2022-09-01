using Microsoft.AspNetCore.Mvc;

namespace EShop.User.Api.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
