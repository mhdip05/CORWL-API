using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CityRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddCity(City city)
        {
            _context.Cities.Add(city);
        }

        public async Task<City> GetCityById(int id)
        {
#nullable disable
            return await _context.Cities.FindAsync(id);
        }

        public async Task<City> GetCityByName(string name, int countryId)
        {
            return await _context.Cities
                .Where(c => c.CityName == name && c.CountryId == countryId)
                .Select(x => new City { Id = x.Id, CityName = x.CityName })
                .SingleOrDefaultAsync();
        }

        public void UpdateCity(City city)
        {
            _context.Attach(city);
        }
    }
}
