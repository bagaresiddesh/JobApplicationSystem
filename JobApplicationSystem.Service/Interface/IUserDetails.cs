using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;

namespace JobApplicationSystem.Service.Interface
{
    public interface IUserDetails
    {
        IEnumerable<UserDetails> GetAll();
        UserDetails GetById(int id);
        int Create(UserDetails userDetails);
        void Update(UserDetails userDetails);
        bool Any(int Id);
        void MyDelete(int id);
        int Count();
    }
}
