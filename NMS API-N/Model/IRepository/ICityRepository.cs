using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
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
