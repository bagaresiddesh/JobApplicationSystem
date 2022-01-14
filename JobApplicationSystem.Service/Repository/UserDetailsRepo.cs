using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationSystem.Service.Repository
{
    public class UserDetailsRepo : IUserDetails
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public int Count()
        {
            return _applicationDbContext.UserDetails.Count();
        }
        public UserDetailsRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Create(UserDetails userDetails)
        {
            _applicationDbContext.UserDetails.Add(userDetails);
        }

        public void Delete(int id)
        {
            UserDetails temp = _applicationDbContext.UserDetails.Find(id);
            _applicationDbContext.UserDetails.Remove(temp);
        }

        public IEnumerable<UserDetails> GetAll()
        {
            return _applicationDbContext.UserDetails.ToList();
        }

        public UserDetails GetById(int id)
        {
            return _applicationDbContext.UserDetails.FirstOrDefault(x => x.Id == id);
        }

        public void Update(UserDetails userDetails)
        {
            _applicationDbContext.Entry(userDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public bool Any(int id)
        {
            if(_applicationDbContext.UserDetails.Any(x=>x.Id==id))
            {
                return true;
            }
            return false;
        }
    }
}
