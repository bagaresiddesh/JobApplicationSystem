using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Model;
using JobApplicationSystem.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationSystem.Service.Repository
{
    public class AddressDetailsRepo : IAddressDetails
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AddressDetailsRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Any(int id)
        {
            if(_applicationDbContext.AddressDetails.Any(x=>x.Id==id))
            {
                return true;
            }
            return false;
        }

        public void Create(AddressDetails addressDetails)
        {
            _applicationDbContext.AddressDetails.Add(addressDetails);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            AddressDetails temp = _applicationDbContext.AddressDetails.Find(id);
            _applicationDbContext.Remove(temp);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<AddressDetails> GetAll()
        {
            return _applicationDbContext.AddressDetails.ToList();
        }

        public AddressDetails GetById(int id)
        {
            return _applicationDbContext.AddressDetails.FirstOrDefault(x => x.Id == id);
        }

        public void Update(AddressDetails addressDetails)
        {
            _applicationDbContext.Entry(addressDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _applicationDbContext.SaveChanges();
        }
    }
}
