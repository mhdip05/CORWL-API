using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.IServices;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Pagination;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
{
    [Authorize("ManagementRole")]
    public class EmployeeController : BaseApiController
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

        [HttpPost("SaveEmployeeBasicInfo")]
        public async Task<IActionResult> SaveEmployeeBasicInfo(EmployeeBasicInfoDto employeeBasicInfoDto)
        {
            var employeeData = _mapper.Map<Employee>(employeeBasicInfoDto);

            employeeData.CreatedBy = int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.SaveEmployeeBasicInfo(employeeData);

            if(res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Employee data saved successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("adding employee basic info"));
        }

        [HttpPut("UpdateEmployeeBasicInfo")]
        public async Task<IActionResult> UpdateEmployeeBasicInfo(EmployeeBasicInfoDto employeeBasicInfoDto)
        {
            employeeBasicInfoDto.UpdatedBy= int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.UpdateEmployeeBasicInfo(employeeBasicInfoDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete()) 
                return Ok(new { Message = "Employee Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating employee"));

        }

        [HttpPost("SaveDocument")]
        public async Task<ActionResult> SaveDocument([FromForm] EmployeeDocumentDto employeeDocumentDto)
        {
            var docInfo = _mapper.Map<EmployeeDocumentMaster>(employeeDocumentDto);
            docInfo.CreatedBy = int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.SaveDocument(docInfo, employeeDocumentDto.Files);

            if (await _uot.Complete())
                  return Ok(res);

            return Ok(ValidationMsg.SomethingWrong("adding employee document info"));
        }

        [HttpPut("UpdateDocumentMaster")]
        public async Task<ActionResult> UpdateDocumentMaster(EmployeeDocumentMaseterDto employeeDocument)
        {
            employeeDocument.UpdatedBy= int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.UpdateEmployeeDocumentMaster(employeeDocument);

            if(res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete()) return Ok(new { Message = "Doc Info Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating Employee Doc Info"));
        }

        [HttpGet("GetDocumentInfoByEmployee/{employeeId}")]
        public async Task<ActionResult> GetDocumentInfoByEmployee(int employeeId)
        {
            return Ok(await _uot.EmployeeRepository.GetDocumentInfoByEmployee(employeeId));
        }

        [HttpDelete("DeleteEmployeeDoc/{fileId}/{empId}")]
        public async Task<ActionResult> DeleteEmployeeDoc(int fileId, int empId)
        {
            var data = await _uot.EmployeeRepository.DeleteEmployeeDoc(fileId, empId);

            if (await _uot.Complete())
                return Ok(data);

            return Ok(ValidationMsg.SomethingWrong("deleting employee document"));
        }
    }
}
