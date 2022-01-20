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
    public class AddressDetailsController : Controller
    {
        private readonly IAddressDetails _addressDetails;

        public AddressDetailsController(IAddressDetails addressDetails)
        {
            _addressDetails= addressDetails;
        }

        // GET: AddressDetails
        public IActionResult Index(string search)
        {
            List<AddressDetails> result = _addressDetails.GetAll().ToList();

            //Searching City/State/Country
            if (search != null)
            {
                result = result.Where(x => x.City.Contains(search) || x.State.Contains(search) || x.Country.Contains(search)).ToList();
            }
            return View(result);
        }

        // GET: AddressDetails/Details/5
        public IActionResult Details(int id,int eid)
        {
            //recieving id from Index of AddressDetails
            if (id != 0 && eid == 0)
            {
                var addressDetails = _addressDetails.GetById(id);
                return View(addressDetails);
            }
            //recieving eid from Details of UserDetails
            else if(id == 0 && eid!=0)
            {
                var addressDetails= _addressDetails.GetByUserDetailsId(eid);
                return View(addressDetails);
            }
            return View();
        }

        // GET: AddressDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserDetailsId,Country,State,City,PostalCode,AddressLine1,AddressLine2")] AddressDetails addressDetails)
        {
            if (ModelState.IsValid)
            {
                //Getting Foreign key form UserDetails 
                int newkey = (int)TempData["NewKey"];

                AddressDetails temp = new AddressDetails
                {
                    UserDetailsId = newkey,
                    Country = addressDetails.Country,
                    State = addressDetails.State,
                    City = addressDetails.City,
                    PostalCode = addressDetails.PostalCode,
                    AddressLine1 = addressDetails.AddressLine1,
                    AddressLine2 = addressDetails.AddressLine2,
                };

                _addressDetails.Create(temp);

                return RedirectToAction("Create","EducationalDetails");
            }
            return View(addressDetails);
        }

        // GET: AddressDetails/Edit/5
        public IActionResult Edit(int id,int eid)
        {
            //recieving id from Edit of UserDetails
            if (id != 0 && eid == 0)
            {
                var addressDetails = _addressDetails.GetByUserDetailsId(id);
                return View(addressDetails);
            }
            //recieving id from Index of AddressDetails
            else if (id == 0 && eid != 0)
            {
                var addressDetails = _addressDetails.GetById(eid);
                return View(addressDetails);
            }
            return View();
        }

        // POST: AddressDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserDetailId,Country,State,City,PostalCode,AddressLine1,AddressLine2")] AddressDetails addressDetails)
        {
            if (id != addressDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _addressDetails.Update(addressDetails);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressDetailsExists(addressDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "EducationalDetails", new { id = id });
            }
            return View(addressDetails);       
        }

        private bool AddressDetailsExists(int id)
        {
            return _addressDetails.Any(id);
        }
    }
}
