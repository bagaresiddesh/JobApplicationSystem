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
    public class EducationalDetailsController : Controller
    {
        private readonly IEducationalDetails _educationalDetails;

        public EducationalDetailsController(IEducationalDetails educationalDetails)
        {
            _educationalDetails = educationalDetails;
        }

        // GET: EducationalDetails
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string project)
        {
            List<EducationalDetails> result = _educationalDetails.GetAll().ToList();

            ViewData["Project"] = project == "Yes" ? "No" : "Yes";
            
            if(project == "Yes")
            {
                result = result.Where(x => x.AcademicProjects != null).ToList();
            }
            
            if (project == "No")
            {
                result = result.Where(x => x.AcademicProjects == null).ToList();
            }
            
            return View(result);
        }

        // GET: EducationalDetails/Details/5
        public IActionResult Details(int id,int uid)
        {
            //recieving id from Index EducationalDetails
            if (id != 0 && uid == 0)
            {
                var educationalDetails = _educationalDetails.GetById(id);
                return View(educationalDetails);
            }
            //recieving uid from View of AddressDetails
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

                //Getting primary key i.e. EId 
                int Ekey = _educationalDetails.Create(temp);

                //Passing EId as Foreig key to Educations
                TempData["EKey"] = Ekey;

                //counter to accept 4 qualification(Educations) forms
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
            //Getting id from Edit of AddressDetails
            if (eid != 0 && id == 0)
            {
                var educationalDetails = _educationalDetails.GetById(eid);
                return View(educationalDetails);
            }
            //Getting eid from Index of EducationalDetails
            //Not Working
            else if (eid == 0 && id != 0)
            {
                var educationalDetails = _educationalDetails.GetByUserDetailsId(id);
                return View(educationalDetails);
            }
            return View();
        }

        // POST: EducationalDetails/Edit/5
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

        private bool EducationalDetailsExists(int id)
        {
            return _educationalDetails.Any(id);
        }
    }
}
