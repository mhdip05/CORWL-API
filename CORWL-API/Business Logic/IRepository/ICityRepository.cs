using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;

namespace CORWL_API.Business_Logic.IRepository
{
    public interface ICityRepository
    {
        void AddCity(City city);
        Task<IEnumerable<CityDto>> GetAllCity();
        Task<IQueryable<object>> GetCityByCountry(int countryId);
        Task<City> GetCityById(int id);
        Task<City> GetCityByName(string name, int countryId);
        void UpdateCity(City city);
    }
}
