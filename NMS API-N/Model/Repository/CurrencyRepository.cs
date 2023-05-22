using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.FetchDTO;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
#nullable disable
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CurrencyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddCurrency(Currency currency)
        {
            _context.Add(currency);
        }

        public async Task<int> DeleteCurrency(int id)
        {
            var currencyData = await _context.Currencies.FindAsync(id);
            if (currencyData == null) return 0;
            else
            {
                _context.Currencies.Remove(currencyData);
                return 1;
            }
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrency()
        {
            return await (from cur in _context.Currencies
                          join usr in _context.Users on cur.CreatedBy equals usr.Id
                          into sbUsr from subUsr in sbUsr.DefaultIfEmpty()
                          select new CurrencyDto
                          {
                              Id = cur.Id,
                              CurrencyName = cur.CurrencyName,
                              CurrencySymbol = cur.CurrencySymbol,
                              CreatedDate = cur.CreatedDate,
                              LastUpdatedDate = cur.LastUpdatedDate,
                              CreatedByName = subUsr.UserName.ToUpper()
                          }).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetCurrencyDropdown()
        {
            return await _context.Currencies
                .OrderBy(e => e.CurrencyName)
                .Select(e => new
                {
                    CurrencyId = e.Id,
                    CurrencyName = e.CurrencyName,

                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Currency> GetCurrencyById(int id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public async Task<Currency> GetCurrencyByName(string currencyName)
        {
            return await _context.Currencies.Where(e => e.CurrencyName == currencyName).SingleOrDefaultAsync();
        }

        public void UpdateCurrency(Currency currency)
        {
            _context.Attach(currency);
        }


    }
}
