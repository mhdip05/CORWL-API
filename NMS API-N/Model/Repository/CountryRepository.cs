using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Helper;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.FetchDTO;
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

        public async Task<IEnumerable<CountryDto>> GetAllCountry()
        {
            return await (from con in _context.Countries
                          join usr in _context.Users on con.CreatedBy equals usr.Id
                          into sbUsr
                          from subUsr in sbUsr.DefaultIfEmpty()
                          select new CountryDto
                          {
                              Id = con.Id,
                              CountryName = con.CountryName,
                              CountryAlias = con.CountryAlias,
                              CreatedDate = con.CreatedDate,
                              LastUpdatedDate = con.LastUpdatedDate,
                              CreatedByName = subUsr.UserName.ToUpper()
                          }).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetCountryDropdown()
        {
            return await _context.Countries
                .Select(x => new 
                {
                    CountryId = x.Id,
                    CountryName = x.CountryName.ToUpper(),
                })
                .AsNoTracking()
                .ToListAsync();
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


        public async Task<Result> UpdateCountry(CountryDto countryDto)
        {
            var existCountry = await GetCountryById(countryDto.Id);

            if (existCountry == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            if (existCountry.CountryName.ToLower() != countryDto.CountryName.ToLower())
            {
                if (await GetCountryByName(countryDto.CountryName.ToLower()) != null)
                {
                    return new Result { Status = false, Message = ValidationMsg.Exist("This country")};
                }
            }

            var countryData = _mapper.Map(countryDto, existCountry);
            countryData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

            _context.Attach(countryData);

            return new Result { Status = true, Data = countryData };
        }
    }
}
