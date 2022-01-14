using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;

namespace JobApplicationSystem.Service.Interface
{
    public interface IAddressDetails
    {
        IEnumerable<AddressDetails> GetAll();
        AddressDetails GetById(int id);
        void Create(AddressDetails addressDetails);
        void Update(AddressDetails addressDetails);
        void Delete(int Id);
        bool Any(int Id);
    }
}
