using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationSystem.Controllers
{
    [Authorize]
    public class OtherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public new IActionResult User()
        {
            return View();
        }
    }
}
