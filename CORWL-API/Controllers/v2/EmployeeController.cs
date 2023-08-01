using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CORWL_API.CustomValidation;
using CORWL_API.DbContext;
using CORWL_API.Extension;
using CORWL_API.IServices;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Pagination;
using CORWL_API.Unit_Of_Work;
using CORWL_API.Helper;

namespace CORWL_API.Controllers.v2
{

    public class EmployeeController : BaseApiController2
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpGet("GetAllEmployee")]
        public async Task<ActionResult<IEnumerable<EmployeeBasicInfoDto>>> GetAllEmployee([FromQuery] PaginationParams @params)
        {
            var employees = await _uot.EmployeeRepository.GetAllEmployee(@params);

            Response.AddPaginationHeader(employees.CurrentPage, employees.PageSize, employees.TotalCount, employees.TotalPages);

            if (employees == null) return Ok(new NoRecordFoundDto());

            return Ok(employees);
        }

        [HttpGet("GetEmployeeBasicInfo/{employeeId}")]
        public async Task<IActionResult> GetEmployeeBasicInfo(int employeeId)
        {
            return Ok(await _uot.EmployeeRepository.GetEmployeeBasicInfo(employeeId));
        }

        [HttpGet("GetEmployeeDropdown")]
        public async Task<IActionResult> GetEmployeeDropdown()
        {
            return Ok(await _uot.EmployeeRepository.GetEmployeeDropdown());
        }

    }


}
