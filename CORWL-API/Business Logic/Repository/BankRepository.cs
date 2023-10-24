using AutoMapper;
using CORWL_API.DbContext;
using CORWL_API.Model.Entities;
using CORWL_API.Business_Logic.IRepository;

namespace CORWL_API.Business_Logic.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BankRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddBank(Bank bank)
        {
            _context.Add(bank);
        }
    }
}
