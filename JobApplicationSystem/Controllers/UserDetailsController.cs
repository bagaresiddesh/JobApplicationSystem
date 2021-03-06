using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace JobApplicationSystem.Controllers
{
    [Authorize]
    public class UserDetailsController : Controller
    {
        private readonly IUserDetails _userDetails;

        public UserDetailsController(IUserDetails userDetails)
        {
            _userDetails = userDetails;
        }

        // GET: UserDetails
        public IActionResult Index(string search, string sortOrder, string delete,string gender)
        {
            //Total to display total applications on index view
            ViewData["Total"] = _userDetails.Count();

            List<UserDetails> result = _userDetails.GetAll().ToList();

            //DatesortParam recieves parameter from index view to sort result date-wise
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (sortOrder == "Date")
            {
                result = result.OrderByDescending(x => x.DateOfBirth).ToList();
            }
            if (sortOrder == "date_desc")
            {
                result = result.OrderBy(x => x.DateOfBirth).ToList();
            }

            //Filtering the resul set by gender 
            if (gender =="Male")
            {
                result = result.Where(x => x.Gender.Equals(Gender.male)).ToList();
            }
            else  if(gender =="Female")
            {
                result = result.Where(x => x.Gender.Equals(Gender.female)).ToList();
            }
            else if (gender == "Other")
            {
                result = result.Where(x => x.Gender.Equals(Gender.other)).ToList();
            }

            //Searching FirstName/LastName and Email
            if (search != null)
            {
                result = result.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.Email.Contains(search)).ToList();
            }

            //getting the id for object to be deleted
            if (delete != null)
            {
                return RedirectToAction("Delete", new { id = delete });
            }
            return View(result);   
        }

        // GET: UserDetails/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = _userDetails.GetById(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // GET: UserDetails/Create
        public IActionResult Create()
        {
            //Checking whether user has applied only once 
            if (ViewBag.Id != null)
            {
                return Content("You have already applied!");
            }
            return View();
        }

        // POST: UserDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,MiddleName,LastName,Gender,DateOfBirth,Email,Phone")] UserDetails userDetails)
        {
            if(ViewBag.Id!=null)
            {
                return Content("You have already applied!");
            }

            if (ModelState.IsValid)
            {
                int newkey = _userDetails.Create(userDetails);

                TempData["NewKey"] = newkey;

                return RedirectToAction("Create", "AddressDetails");
            }
            return View(userDetails);
        }

        // GET: UserDetails/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = _userDetails.GetById(id);
            if (userDetails == null)
            {
                return NotFound();
            }
            return View(userDetails);
        }

        // POST: UserDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Gender,DateOfBirth,Email,Phone")] UserDetails userDetails)
        {
            if (id != userDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userDetails.Update(userDetails);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailsExists(userDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "AddressDetails", new {id=id});
            }
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public IActionResult Delete(int id)
        {
            var userDetails = _userDetails.GetById(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //Performing delete which will further delete all the references of the object
            var userDetails = _userDetails.GetById(id);
            if(userDetails == null)
            {
                return NotFound();
            }
            _userDetails.MyDelete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool UserDetailsExists(int id)
        {
            return _userDetails.Any(id);
        }

    }
}
