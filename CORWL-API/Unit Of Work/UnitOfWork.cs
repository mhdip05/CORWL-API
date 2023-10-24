using AutoMapper;
using CORWL_API.DbContext;
using CORWL_API.Business_Logic.IRepository;
using CORWL_API.Business_Logic.Repository;

namespace CORWL_API.Unit_Of_Work
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
        public IUserRepository UserRepository => new UserRepository(_context, _mapper);
        public ICurrencyRepository CurrencyRepository => new CurrencyRepository(_context, _mapper);
        public IBankRepository BankRepository => new BankRepository(_context, _mapper);
        public IBranchReposiroty BranchReposiroty => new BranchRepository(_context, _mapper);
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_context, _mapper);
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_context, _mapper);
        public IDesignationRepository DesignationRepository => new DesignationRepository(_context, _mapper);
        public ISupplierRepository SupplierRepository => new SupplierRepository(_context, _mapper);

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
