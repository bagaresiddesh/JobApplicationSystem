using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationSystem.Service.Repository
{
    public class EducationalDetailsRepo : IEducationalDetails
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EducationalDetailsRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Any(int id)
        {
            if(_applicationDbContext.EducationalDetails.Any(x=>x.Id==id))
            {
                return true;
            }
            return false;   
        }

        public void Create(EducationalDetails educationalDetails)
        {
            _applicationDbContext.EducationalDetails.Add(educationalDetails);
        }

        public void Delete(int id)
        {
            EducationalDetails temp = _applicationDbContext.EducationalDetails.Find(id);
            _applicationDbContext.EducationalDetails.Remove(temp);
        }

        public IEnumerable<EducationalDetails> GetAll()
        {
            return _applicationDbContext.EducationalDetails.ToList();
        }

        public EducationalDetails GetById(int id)
        {
            return _applicationDbContext.EducationalDetails.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(EducationalDetails educationalDetails)
        {
            _applicationDbContext.Entry(educationalDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
