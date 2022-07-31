using AutoMapper;
using NMS_API_N.DbContext;
using NMS_API_N.Model.IRepository;
using NMS_API_N.Model.Repository;

namespace NMS_API_N.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICompanyRepository CompanyRepository => new CompanyRepository(_context, _mapper);

        public IAddressRepository AddressRepository => new AddressRepository(_context, _mapper);

        public ICountryRepository CountryRepository => new CountryRepository(_context, _mapper);

        public ICityRepository CityRepository => new CityRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
