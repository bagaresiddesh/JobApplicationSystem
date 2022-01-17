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

        public void Delete(int id)
        {
            UserDetails temp = _applicationDbContext.UserDetails.Find(id);
            _applicationDbContext.UserDetails.Remove(temp);
            _applicationDbContext.SaveChanges();
        }
         public void MyDelete(int id)
        {
            UserDetails temp = _applicationDbContext.UserDetails.Where(x=>x.Id==id).FirstOrDefault();
            AddressDetails target = _applicationDbContext.AddressDetails.Where(x => x.UserDetailsId == id).FirstOrDefault();            
            EducationalDetails target2 = _applicationDbContext.EducationalDetails.Where(x => x.UserDetailsId == id).FirstOrDefault();

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

        public IEnumerable<UserDetails> GetComplete()
        {
            var result = _applicationDbContext.UserDetails.ToList();
            int id=0,eid=0;

            foreach (var item in result)
            {
              
            }

            return result;
        }

        //public List<Check> InnerJoinUser()
        //{
        //    List<Check> innerJoin = new List<Check>();
            
        //    var r= from ud in _applicationDbContext.UserDetails // outer sequence
        //                    join ad in _applicationDbContext.AddressDetails
        //                     on ud.Id equals ad.UserDetailsId
        //                    join ed in _applicationDbContext.EducationalDetails
        //                     on ud.Id equals ed.UserDetailsId
                           
        //                    select new Check
        //                    { // result selector 
        //                       Id = ud.Id,
        //                       AddressId= ad.Id,
        //                       EductionDetailId=ed.UserDetailsId,
        //                       UserId=ed.UserDetailsId,
        //                       Name=ud.FirstName

        //                    };
        //    innerJoin.AddRange(innerJoin);
        //    return innerJoin;
        //}

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
