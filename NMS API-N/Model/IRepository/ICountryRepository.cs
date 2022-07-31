using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);
        Task<Country> GetCountryById(int id);
        Task<Country> GetCountryByName(string name);
        void UpdateCountry(Country country);
    }
}
