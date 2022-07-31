using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private IMapper _mapper;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddCountry(Country country)
        {
            _context.Countries.Add(country);
        }

        public async Task<Country> GetCountryById(int id)
        {
#nullable disable
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> GetCountryByName(string countryName)
        {
            return await _context.Countries
               .Where(x => x.CountryName == countryName)
               .Select(x => new Country { Id = x.Id, CountryName = x.CountryName })
               .SingleOrDefaultAsync();
        }

        public void UpdateCountry(Country country)
        {
            _context.Attach(country);
        }
    }
}
