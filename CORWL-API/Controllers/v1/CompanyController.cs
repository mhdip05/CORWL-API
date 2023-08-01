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
    public class CompanyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _unitOfWork.CompanyRepository.GetAllCompanies());
        }
        [HttpGet("GetCompanyDropdown")]
        public async Task<IActionResult> GetCompanyDropdown()
        {
            return Ok(await _unitOfWork.CompanyRepository.GetCompanyDropdown());
        }

        [HttpGet("GetCompanyById/{id}")]
        public async Task<ActionResult> GetCompanyById(int id)
        {
            var companyData = await _unitOfWork.CompanyRepository.GetCompayByIdAsync(id);
            if (companyData == null) return BadRequest("No Data Found");
            return Ok(companyData);
        }

        [HttpPost("add-company")]
        public async Task<ActionResult> AddCompany(CompanyDto companyDto)
        {
            var companyData = _mapper.Map<Company>(companyDto);

            companyData.CreatedBy = int.Parse(User.GetUserId());

            var res = await _unitOfWork.CompanyRepository.AddCompany(companyData);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _unitOfWork.Complete())
                return Ok(res.Data);

            return BadRequest(ValidationMsg.SomethingWrong("adding company"));
        }

        [HttpPut("update-company")]
        public async Task<ActionResult> UpdateCompany(CompanyDto companyDto)
        {
            companyDto.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _unitOfWork.CompanyRepository.UpdateCompany(companyDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _unitOfWork.Complete()) return Ok(res.Data);

            return BadRequest(ValidationMsg.SomethingWrong("updating company"));
        }

        [HttpGet("DeleteCompany")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            var result = await _unitOfWork.CompanyRepository.DeleteCompany(id);

            if (result.Status == false) return BadRequest(result.Message);

            if (await _unitOfWork.Complete()) return Ok(new SuccessMessageDto { Message = "Company Deleted Successfully." });

            return BadRequest(ValidationMsg.SomethingWrong("deleteing company"));
        }
    }
}
