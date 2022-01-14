﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;

namespace JobApplicationSystem.Controllers
{
    public class AddressDetailsController : Controller
    {
        private readonly IAddressDetails _addressDetails;

        public AddressDetailsController(IAddressDetails addressDetails)
        {
            _addressDetails= addressDetails;
        }

        // GET: AddressDetails
        public IActionResult Index()
        {
            return View(_addressDetails.GetAll());
        }

        // GET: AddressDetails/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressDetails = _addressDetails.GetById(id);
            if(addressDetails == null)
            {
                return NotFound();
            }

            return View(addressDetails);
        }

        // GET: AddressDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserDetailsId,Country,State,City,PostalCode,AddressLine1,AddressLine2")] AddressDetails addressDetails)
        {
            if (ModelState.IsValid)
            {
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

                return RedirectToAction(nameof(Index));
            }
            return View(addressDetails);
        }

        // GET: AddressDetails/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressDetail = _addressDetails.GetById(id);
            if (addressDetail == null)
            {
                return NotFound();
            }
            return View(addressDetail);
        }

        // POST: AddressDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
            }
            return View(addressDetails);
        }

        // GET: AddressDetails/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressDetails = _addressDetails.GetById(id);
            if (addressDetails == null)
            {
                return NotFound();
            }

            return View(addressDetails);
        }

        // POST: AddressDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var addressDetails = _addressDetails.GetById(id);
            _addressDetails.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool AddressDetailsExists(int id)
        {
            return _addressDetails.Any(id);
        }
    }
}