using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;
using System.Linq;
using System.Collections.Generic;

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
        public IActionResult Index(string search)
        {
            List<EducationalDetails> result = _educationalDetails.GetAll().ToList();

            if (search != null)
            {
                result = result.Where(x => x.GraduationPassingYear.Contains(search)).ToList();
            }
            return View(result);
        }

        // GET: EducationalDetails/Details/5
        public IActionResult Details(int id)
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
        public IActionResult Create([Bind("Id,UserDetailsId,SSCPassingYear,HSCPassingYear,GraduationPassingYear,PostGraduationPassingYear,IsYearGap,IsActiveBacklogs,AcademicProjects")] EducationalDetails educationalDetails)
        {
            if (ModelState.IsValid)
            {
                int newkey = (int)TempData["NewKey"];

                EducationalDetails temp = new EducationalDetails
                {
                    UserDetailsId = newkey,
                    SSCPassingYear = educationalDetails.SSCPassingYear,
                    HSCPassingYear = educationalDetails.HSCPassingYear,
                    GraduationPassingYear = educationalDetails.GraduationPassingYear,
                    PostGraduationPassingYear = educationalDetails.PostGraduationPassingYear,
                    IsYearGap = educationalDetails.IsYearGap,
                    IsActiveBacklogs = educationalDetails.IsActiveBacklogs,
                    AcademicProjects = educationalDetails.AcademicProjects
                };

                _educationalDetails.Create(temp);

                return RedirectToAction("Index", "Home");
            }
            return View(educationalDetails);
        }

        // GET: EducationalDetails/Edit/5
        public IActionResult Edit(int id)
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

        // POST: EducationalDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserDetailsId,SSCPassingYear,HSCPassingYear,GraduationPassingYear,PostGraduationPassingYear,IsYearGap,IsActiveBacklogs,AcademicProjects")] EducationalDetails educationalDetails)
        {
            if (id != educationalDetails.Id)
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
                    if (!EducationalDetailsExists(educationalDetails.Id))
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
