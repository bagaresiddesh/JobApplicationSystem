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

        public IEnumerable<AddressDetails> GetAll()
        {
            return _applicationDbContext.AddressDetails.ToList();
        }

        public AddressDetails GetById(int id)
        {
            return _applicationDbContext.AddressDetails.FirstOrDefault(x => x.Id == id);
        }

        //returns object by foreign key i.e. UserDetailsId
        public AddressDetails GetByUserDetailsId(int uid)
        {
            return _applicationDbContext.AddressDetails.FirstOrDefault(x => x.UserDetailsId == uid);
        }

        public void Update(AddressDetails addressDetails)
        {
            //getting old data from object and updating its properties
            AddressDetails oldData =_applicationDbContext.AddressDetails.Where(x=>x.Id == addressDetails.Id).FirstOrDefault();
            if (oldData != null)
            {
                oldData.Country = addressDetails.Country;
                oldData.State = addressDetails.State;
                oldData.City = addressDetails.City;
                oldData.PostalCode = addressDetails.PostalCode;
                oldData.AddressLine1 = addressDetails.AddressLine1;
                oldData.AddressLine2 = addressDetails.AddressLine2;
            }
            _applicationDbContext.SaveChanges();
        }
    }
}
