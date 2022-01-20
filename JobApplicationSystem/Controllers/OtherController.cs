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

            //Getting username(mail) where user is logged in 
            var username = User.Identity.Name;


            var userDetails = _userDetails.GetAll().ToList();

            //Searching username(mail) in our UserDetails records
            var target = userDetails.FirstOrDefault(x => x.Email == username);

            //If username(mail) found
            if (target != null)
            {
                id = target.Id;
            }

            //Sending this Id of username(mail) to UserDetails 
            ViewBag.Id = id;    

            return View();
        }
    }
}
