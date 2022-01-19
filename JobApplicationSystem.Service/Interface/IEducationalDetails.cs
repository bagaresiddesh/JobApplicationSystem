using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;

namespace JobApplicationSystem.Service.Interface
{
    public interface IEducationalDetails
    {
        IEnumerable<EducationalDetails> GetAll();
        EducationalDetails GetById(int id);
        EducationalDetails GetByUserDetailsId(int uid);
        int Create(EducationalDetails educationalDetails);
        void Update(EducationalDetails educationalDetails);
        bool Any(int Id);
    }
}
