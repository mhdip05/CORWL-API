using AutoMapper;
using NMS_API_N.DbContext;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
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
