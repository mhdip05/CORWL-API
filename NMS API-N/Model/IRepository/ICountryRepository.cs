using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.FetchDTO;

namespace NMS_API_N.Model.IRepository
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);
        Task<Country> GetCountryById(int id);
        Task<Country> GetCountryByName(string name);
        Task<Result> UpdateCountry(CountryDto countryDto);
        Task<IEnumerable<CountryDto>> GetAllCountry();
        Task<IEnumerable<object>> GetCountryDropdown();
    }
}
