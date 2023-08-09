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

namespace CORWL_API.Controllers.v1
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

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Employee data saved successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("adding employee basic info"));
        }

        [HttpPut("UpdateEmployeeBasicInfo")]
        public async Task<IActionResult> UpdateEmployeeBasicInfo(EmployeeBasicInfoDto employeeBasicInfoDto)
        {
            employeeBasicInfoDto.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.UpdateEmployeeBasicInfo(employeeBasicInfoDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Employee Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating employee"));

        }
        [HttpPost("SaveEmployeeJobDetails")]
        public async Task<IActionResult> SaveEmployeeJobDetails(EmployeeJobDetailsDto employeeJobDetailsDto)
        {
            var data = _mapper.Map<EmployeeJobDetails>(employeeJobDetailsDto);
            data.CreatedBy = int.Parse(User.GetUserId());
            data.CreatedDate = DateTime.Now;

            var res = await _uot.EmployeeRepository.SaveEmployeeJobDetails(data);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Employee Job Details Saved Successfully" });

            return BadRequest(ValidationMsg.SomethingWrong());
        }

        [HttpPut("UpdateEmployeeJobDetails")]
        public async Task<IActionResult> UpdateEmployeeJobDetails(EmployeeJobDetailsDto employeeJobDetails)
        {
            employeeJobDetails.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.UpdateEmployeeJobDetails(employeeJobDetails);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Job Details Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating employee"));

        }

        [HttpGet("GetEmployeeJobDetails/{employeeId}")]
        public async Task<IActionResult> GetEmployeeJobDetails(int employeeId)
        {
            return Ok(await _uot.EmployeeRepository.GetEmployeeJobDetails(employeeId));
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
            employeeDocument.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.UpdateEmployeeDocumentMaster(employeeDocument);

            if (res.Status == false) return BadRequest(res.Message);

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
