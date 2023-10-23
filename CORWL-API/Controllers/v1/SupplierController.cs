using AutoMapper;
using CORWL_API.Unit_Of_Work;

namespace CORWL_API.Controllers.v1
{
    public class SupplierController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public SupplierController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }


    }
}
