using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;

namespace JobApplicationSystem.Service.Interface
{
    public interface IEducationalDetails
    {
        IEnumerable<EducationalDetails> GetAll();
        EducationalDetails GetById(int id);
        void Create(EducationalDetails educationalDetails);
        void Update(EducationalDetails educationalDetails);
        void Delete(int Id);
        bool Any(int Id);
    }
}
