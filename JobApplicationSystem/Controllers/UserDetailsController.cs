using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationSystem.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly IUserDetails _userDetails;

        public UserDetailsController(IUserDetails userDetails)
        {
            _userDetails = userDetails;
        }

        // GET: UserDetails
        public IActionResult Index(string search)
        {
            List<UserDetails> result= _userDetails.GetAll().ToList();
            if(search != null)
            {
                result = result.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.Email.Contains(search)).ToList();
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
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,MiddleName,LastName,Gender,DateOfBirth,Email,Phone")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                int newkey=_userDetails.Create(userDetails);
          

                TempData["NewKey"]=newkey;

                return RedirectToAction("Create","AddressDetails");
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
            }
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public IActionResult Delete(int id)
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

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userDetails = _userDetails.GetById(id);
            _userDetails.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool UserDetailsExists(int id)
        {
            return _userDetails.Any(id);
        }

        
    }
}
