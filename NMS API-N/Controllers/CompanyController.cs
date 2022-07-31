using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Extension;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
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

        [HttpPost("add-company")]
        public async Task<ActionResult<CompanyDto>> AddCompany(CompanyDto company)
        {
            var companyData = _mapper.Map<Company>(company);

            var checkCompany = await _unitOfWork.CompanyRepository.GetComanyByCompanyName(companyData.CompanyName.ToLower());

            if (checkCompany != null) return BadRequest("Company is Exist");
                    
            companyData.CompanyName = companyData.CompanyName.ToLower();
            companyData.CreatedBy = int.Parse(User.GetUserId());

            _unitOfWork.CompanyRepository.AddCompany(companyData);

            if (await _unitOfWork.Complete())
                   return Ok(companyData);

            return BadRequest("Failed to Add Company");

        }

        [HttpPut("update-company")]
        public async Task<ActionResult> UpdateCompany(CompanyDto companyDto)
        {           
            var CompanyData = await _unitOfWork.CompanyRepository.GetCompayByIdAsync(companyDto.CompanyId);

            if (CompanyData == null) return BadRequest("No Data Found");
          
            if (CompanyData.CompanyName == companyDto.CompanyName.ToLower()) return BadRequest("Updaing with same company name is not allowed");

            var data = _mapper.Map(companyDto, CompanyData);

            data.UpdatedBy = int.Parse(User.GetUserId());
            data.LastUpdatedDate = DateTime.Now;
            data.UpdatedCount += 1; 

            _unitOfWork.CompanyRepository.UpdateCompany(CompanyData);

            if(await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update company");

        }
    }
}
