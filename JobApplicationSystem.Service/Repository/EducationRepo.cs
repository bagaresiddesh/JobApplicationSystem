using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationSystem.Service.Repository
{
    public class EducationRepo : IEducation
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EducationRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public bool Any(int Id)
        {
            if (_applicationDbContext.Education.Any(x => x.Id == Id))
            {
                return true;
            }
            return false;
        }

        public void Create(Education education)
        {
            _applicationDbContext.Education.Add(education);
            _applicationDbContext.SaveChanges();

            int key=education.EId;

            EducationalDetails target=_applicationDbContext.EducationalDetails.Find(key);

            int id = target.UserDetailsId;        

        }

        public void Delete(int Id)
        {
            DAL.Model.Education temp = _applicationDbContext.Education.Find(Id);
            _applicationDbContext.Remove(temp);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Education> GetAll()
        {
            return _applicationDbContext.Education.ToList();
        }

        public Education GetById(int Id)
        {
            return _applicationDbContext.Education.FirstOrDefault(x => x.Id == Id);
        }

        public void Update(Education education)
        {
            _applicationDbContext.Entry(education).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _applicationDbContext.SaveChanges();
        }
    }
}
