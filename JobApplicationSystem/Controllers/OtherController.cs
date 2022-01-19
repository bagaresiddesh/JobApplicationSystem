using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JobApplicationSystem.Service.Interface;
using System.Linq;

namespace JobApplicationSystem.Controllers
{
    [Authorize]
    public class OtherController : Controller
    {
        private readonly IUserDetails _userDetails;

        public OtherController(IUserDetails userDetails)
        {
            _userDetails = userDetails; 
        }
        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Users()
        {
            int id=0;

            var username = User.Identity.Name;

            var userDetails = _userDetails.GetAll().ToList();
            var target = userDetails.FirstOrDefault(x => x.Email == username);

            if(target != null)
            {
                id = target.Id;
            }
            

            ViewBag.Id = id;    

            return View();
        }
    }
}
