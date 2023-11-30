using CORWL_API.Model.Entities;

namespace CORWL_API.Business_Logic.IRepository
{
    public interface IAddressRepository
    {
        void AddAddress(Address address);
        Task<Address> GetAddressByIdAsynch(int id);
        void UpdateAddress(Address address);
        Task<Address> CheckAddress(int sourceId, string sourceType, int cityId);
    }
}
