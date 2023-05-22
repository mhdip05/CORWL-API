
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.DTO;
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

        public async Task<IEnumerable<CityDto>> GetAllCity()
        {
            return await (from country in _context.Countries
                          join city in _context.Cities on country.Id equals city.CountryId
                         
                          join usr in _context.Users on city.CreatedBy equals usr.Id
                          into sbUsr
                          from subUsr in sbUsr.DefaultIfEmpty()

                          orderby country.CountryName, city.CityName
                          select new CityDto
                          {
                              CountryId = city.CountryId,
                              CountryName = country.CountryName.ToUpper(),
                              Id = city.Id,
                              CityName = city.CityName.ToUpper(),
                              CreatedBy = city.CreatedBy,
                              CreatedByName = subUsr.UserName.ToUpper(),
                              CreatedDate = city.CreatedDate,
                              LastUpdatedDate = city.LastUpdatedDate,
                          })
                          .AsNoTracking()
                          .ToListAsync();

        }

        public async Task<IQueryable<object>> GetCityByCountry(int countryId)
        {
            var data = await (from city in _context.Cities.Where(e => e.CountryId == countryId)
                              orderby city.CityName
                              select new
                              {
                                  CityId = city.Id,
                                  CityName = city.CityName.ToUpper(),
                              })
                             .AsNoTracking()
                             .ToListAsync();

            return data.AsQueryable();

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
