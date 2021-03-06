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
        public UserDetailsRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public int Count()
        {
            return _applicationDbContext.UserDetails.ToList().Count;
        }

        public int Create(UserDetails userDetails)
        {
            _applicationDbContext.UserDetails.Add(userDetails);
            _applicationDbContext.SaveChanges();
            var a=userDetails.Id;
            return a;
        }

        //Customized cascade delete function
         public void MyDelete(int id)
        {
            UserDetails temp = _applicationDbContext.UserDetails.Where(x=>x.Id==id).FirstOrDefault();
            AddressDetails target = _applicationDbContext.AddressDetails.Where(x => x.UserDetailsId == id).FirstOrDefault();            
            EducationalDetails target2 = _applicationDbContext.EducationalDetails.Where(x => x.UserDetailsId == id).FirstOrDefault();

            //using flag to iterate delete multiple values in Educations table 
            int flag=0;
            while (flag != 1)
            {
                int Eid = target2.EId;
                Education target3 = _applicationDbContext.Education.Where(x => x.EId == Eid).FirstOrDefault();

                if (target3==null)
                {
                    flag=1;
                    break;
                }

                _applicationDbContext.Education.Remove(target3);
                _applicationDbContext.SaveChanges();
            }
            
            _applicationDbContext.UserDetails.Remove(temp);
            _applicationDbContext.AddressDetails.Remove(target);
            _applicationDbContext.AddressDetails.Remove(target);

            _applicationDbContext.SaveChanges();
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
