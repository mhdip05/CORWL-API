using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.IServices;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
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

        [HttpPost("SaveDocument")]
        public async Task<ActionResult> SaveDocument([FromForm] EmployeeDocumentDto employeeDocumentDto)
        {
            var docInfo = _mapper.Map<EmployeeDocumentMaster>(employeeDocumentDto);
            docInfo.CreatedBy = int.Parse(User.GetUserId());

            var res = await _uot.EmployeeRepository.SaveDocument(docInfo, employeeDocumentDto.Files);

            if (await _uot.Complete())
            {
                return Ok(res);
            }

            return Ok(new { res.Status });
        }
    }
}
