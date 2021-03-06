using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;
using JobApplicationSystem.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace JobApplicationSystem.Controllers
{
    [Authorize]
    public class EducationsController : Controller
    {
        private readonly IEducation _education;

        public EducationsController(IEducation education)
        {
            _education = education;
        }
        // GET: Educations
        public IActionResult Index()
        {
            List<Education> education = _education.GetAll().ToList();
            return View(education);
        }

        // GET: Educations/Details/5
        public IActionResult MyDetails(int id,int eid)
        {
            //Getting id from Index of Educations
            if (id != 0 && eid == 0)
            {
                var education = _education.GetAll().Where(x => x.EId == id).ToList();
                if(education==null)
                {
                    ModelState.AddModelError("", "No Details");
                    return View();
                }
                return View(education);                
            }
            //Getting eid from View of EducationalDetails
            else if (id == 0 && eid != 0)
            {
                var education = _education.GetAll().Where(x => x.EId == eid).ToList();
                
               return View(education);
            }
            return View();
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Educations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,EId,Qualification,PassingYear,Percentage")] Education education,int flag)
        {
            if (ModelState.IsValid)
            {
                //Getting Foreign key i.e. EId
                int newkey = (int)TempData["EKey"];

                //Getting Count to accept Qualifications forms 4 times
                int count = (int)TempData["Count"];

                if (count > 0)
                {
                    Education temp = new Education
                    {
                        EId = newkey,
                        Qualification = education.Qualification,
                        PassingYear = education.PassingYear,
                        Percentage = education.Percentage
                    };

                    _education.Create(temp);

                    count = count - 1;

                    TempData["Count"] = count;
                    ViewBag.Count = count;

                    int Id=(int)TempData["UserDetailsId"];

                    if (flag == 1)
                    {
                        return RedirectToAction("Details", "UserDetails", new { id = Id }); ;
                    }

                    if (count == 0)
                    {
                        return RedirectToAction("Details", "UserDetails", new {id=Id});
                    }
                    return RedirectToAction("Create", "Educations");
                }
            }
            return View(education);
        }

        // GET: Educations/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = _education.GetById(id);
            if (education == null)
            {
                return NotFound();
            }
            return View(education);
        }

        // POST: Educations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,EId,Qualification,PassingYear,Percentage")] Education education)
        {
            if (id != education.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _education.Update(education);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
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
            return View(education);
        }

        private bool EducationExists(int id)
        {
            return _education.Any(id);
        }
    }
}
