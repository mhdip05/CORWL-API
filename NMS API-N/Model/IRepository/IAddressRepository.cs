using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface IAddressRepository
    {
        void AddAddress(Address address);
        Task<Address> GetAddressByIdAsynch(int id);
        void UpdateAddress(Address address);
        Task<Address> CheckAddress(int sourceId, string sourceType, int cityId);
    }
}
