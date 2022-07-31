using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface ICityRepository
    {
        void AddCity(City city);
        Task<City> GetCityById(int id);
        Task<City> GetCityByName(string name, int countryId);
        void UpdateCity(City city);
    }
}
