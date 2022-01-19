using JobApplicationSystem.DAL.Model;
using System.Collections.Generic;

namespace JobApplicationSystem.Service.Interface
{
    public interface IAddressDetails
    {
        IEnumerable<AddressDetails> GetAll();
        AddressDetails GetById(int Id);
        AddressDetails GetByUserDetailsId(int UId);
        void Create(AddressDetails addressDetails);
        void Update(AddressDetails addressDetails);
        bool Any(int Id);
    }
}
