﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;
using JobApplicationSystem.Service.Interface;

namespace JobApplicationSystem.Controllers
{
    public class EducationsController : Controller
    {
        private readonly IEducation _education;

        public EducationsController(IEducation education)
        {
            _education= education;
        }

        // GET: Educations
        public IActionResult Index()
        {
            List<Education> education = _education.GetAll().ToList();
            return View(education);
        }

        // GET: Educations/Details/5
        public IActionResult Details(int id)
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

        // GET: Educations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,EId,Qualification,PssingYear,Percentage")] Education education)
        {
            if (ModelState.IsValid)
            {
                int newkey = (int)TempData["EKey"];

                Education temp = new Education
                {
                    EId = newkey,
                    Qualification = education.Qualification,
                    PassingYear = education.PassingYear,
                    Percentage = education.Percentage
                };

                _education.Create(temp);
                return RedirectToAction("Index");
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,EId,Qualification,PssingYear,Percentage")] Education education)
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

        // GET: Educations/Delete/5
        public IActionResult Delete(int id)
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

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var education =  _education.GetById(id);
            _education.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _education.Any(id);
        }
    }
}
