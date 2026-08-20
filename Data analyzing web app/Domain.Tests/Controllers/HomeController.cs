using Domain.Tests.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Tests.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }
    }
}
