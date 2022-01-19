using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;
using JobApplicationSystem.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace JobApplicationSystem.Controllers
{
    public class EducationalDetailsController : Controller
    {
        private readonly IEducationalDetails _educationalDetails;

        public EducationalDetailsController(IEducationalDetails educationalDetails)
        {
            _educationalDetails = educationalDetails;
        }

        // GET: EducationalDetails
        public IActionResult Index()
        {
            List<EducationalDetails> educationalDetails = _educationalDetails.GetAll().ToList();
            return View(educationalDetails);
        }

        // GET: EducationalDetails/Details/5
        public IActionResult Details(int id,int uid)
        {
            if (id != 0 && uid == 0)
            {
                var educationalDetails = _educationalDetails.GetById(id);
                return View(educationalDetails);
            }
            else if (id == 0 && uid != 0)
            {
                var educationalDetails = _educationalDetails.GetByUserDetailsId(uid);
                return View(educationalDetails);
            }
            return View();
        }

        // GET: EducationalDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationalDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserDetailsId,IsYearGap,IsActiveBacklogs,AcademicProjects")] EducationalDetails educationalDetails)
        {
            if (ModelState.IsValid)
            {
                int newkey = (int)TempData["NewKey"];

                EducationalDetails temp = new EducationalDetails
                {
                    UserDetailsId = newkey,
                    IsYearGap = educationalDetails.IsYearGap,
                    IsActiveBacklogs = educationalDetails.IsActiveBacklogs,
                    AcademicProjects = educationalDetails.AcademicProjects
                };

                int Ekey = _educationalDetails.Create(temp);

                TempData["EKey"] = Ekey;

                //counter to accept 3 qualification forms
                int count = 4;
                TempData["Count"] = count;
                ViewBag.Count = count;

                TempData["UserDetailsId"] = newkey;

                return RedirectToAction("Create", "Educations");
            }

            return View(educationalDetails);
        }

        // GET: EducationalDetails/Edit/5
        public IActionResult Edit(int id,int eid)
        {
            if (eid != 0 && id == 0)
            {
                var educationalDetails = _educationalDetails.GetById(eid);
                return View(educationalDetails);
            }
            ////////////////////////////////////////////////////////////////
            else if (eid == 0 && id != 0)
            {
                var educationalDetails = _educationalDetails.GetByUserDetailsId(id);
                return View(educationalDetails);
            }
            return View();
        }

        // POST: EducationalDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EId,UserDetailsId,IsYearGap,IsActiveBacklogs,AcademicProjects")] EducationalDetails educationalDetails)
        {
            if (id != educationalDetails.EId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _educationalDetails.Update(educationalDetails);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationalDetailsExists(educationalDetails.EId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Users","Other");
            }
            return View(educationalDetails);
        }

        // GET: EducationalDetails/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalDetails = _educationalDetails.GetById(id);
            if (educationalDetails == null)
            {
                return NotFound();
            }

            return View(educationalDetails);
        }

        // POST: EducationalDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var educationalDetails = _educationalDetails.GetById(id);
            _educationalDetails.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool EducationalDetailsExists(int id)
        {
            return _educationalDetails.Any(id);
        }
    }
}
