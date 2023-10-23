using AutoMapper;
using CORWL_API.DbContext;
using CORWL_API.Model.IRepository;

namespace CORWL_API.Model.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private DataContext _context;
        private IMapper _mapper;

        public SupplierRepository(DataContext context, IMapper mappper)
        {
            _context = context;
            _mapper = mappper;
        }
    }
}
