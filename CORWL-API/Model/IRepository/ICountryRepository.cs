using CORWL_API.CustomValidation;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Model.FetchDTO;

namespace CORWL_API.Model.IRepository
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
