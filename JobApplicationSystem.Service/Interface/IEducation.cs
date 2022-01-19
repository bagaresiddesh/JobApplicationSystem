using System.Collections.Generic;
using JobApplicationSystem.DAL.Model;

namespace JobApplicationSystem.Service.Interface
{
    public interface IEducation
    {
        IEnumerable<Education> GetAll();
        Education GetById(int Id);
        Education GetByEId(int Id);
        void Create(Education education);
        void Update(Education education);
        bool Any(int Id);
    }
}


