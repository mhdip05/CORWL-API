
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CORWL_API.CustomValidation;
using CORWL_API.Extension;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;

namespace CORWL_API.Controllers.v1
{
    [Authorize("ManagementRole")]
    public class DepartmentController : BaseApiController
    {
        private IUnitOfWork _uot;
        private IMapper _mapper;

        public DepartmentController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpGet("GetAllDepartment")]
        public async Task<IActionResult> GetAllDepartment()
        {
            return Ok(await _uot.DepartmentRepository.GetAllDepartment());
        }

        [HttpGet("GetDepartmentDropdown")]
        public async Task<IActionResult> GetDepartmentDropdown()
        {
            return Ok(await _uot.DepartmentRepository.GetDepartmentDropdown());
        }

        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            return Ok(await _uot.DepartmentRepository.GetDepartmentById(id));
        }

        [HttpPost("AddDepartment")]
        public async Task<ActionResult> AddDepartment(DepartmentDto departmentDto)
        {
            var departmentData = _mapper.Map<Department>(departmentDto);

            departmentData.CreatedBy = int.Parse(User.GetUserId());

            var res = await _uot.DepartmentRepository.AddDepartment(departmentData);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Department added successfully", res.Data, });

            return BadRequest(ValidationMsg.SomethingWrong("adding Department"));
        }


        [HttpPut("UpdateDepartment")]
        public async Task<ActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {
            departmentDto.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.DepartmentRepository.UpdateDepartment(departmentDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete()) return Ok(new { Message = "Department Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating Department"));
        }
    }
}
