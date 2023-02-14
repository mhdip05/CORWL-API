using AutoMapper;
using NMS_API_N.DbContext;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class BranchRepository : IBranchReposiroty
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BranchRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
