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
            if(_applicationDbContext.EducationalDetails.Any(x=>x.EId==id))
            {
                return true;
            }
            return false;   
        }

        public int Create(EducationalDetails educationalDetails)
        {
            _applicationDbContext.EducationalDetails.Add(educationalDetails);
            _applicationDbContext.SaveChanges();
            
            var a = educationalDetails.EId;
            return a;
        }

        public IEnumerable<EducationalDetails> GetAll()
        {
            return _applicationDbContext.EducationalDetails.ToList();
        }

        public EducationalDetails GetById(int id)
        {
            return _applicationDbContext.EducationalDetails.FirstOrDefault(x => x.EId == id);
        }

        public EducationalDetails GetByUserDetailsId(int id)
        {
            return _applicationDbContext.EducationalDetails.FirstOrDefault(x => x.UserDetailsId == id);
        }

        public void Update(EducationalDetails educationalDetails)
        {
            EducationalDetails oldData = _applicationDbContext.EducationalDetails.Where(x => x.EId == educationalDetails.EId).FirstOrDefault();
            if(oldData != null)
            {
                oldData.IsActiveBacklogs = educationalDetails.IsActiveBacklogs;
                oldData.IsYearGap = educationalDetails.IsYearGap;
                oldData.AcademicProjects=educationalDetails.AcademicProjects;
            }
            _applicationDbContext.SaveChanges();
        }
    }
}
