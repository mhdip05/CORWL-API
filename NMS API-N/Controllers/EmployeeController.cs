using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.DbContext;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpGet("GetEmployeeDropdown")]

        public async Task<IActionResult> GetEmployeeDropdown()
        {
            return Ok(await _uot.EmployeeRepository.GetEmployeeDropdown());
        }
    }
}
