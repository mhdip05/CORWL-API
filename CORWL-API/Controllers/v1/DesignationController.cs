using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CORWL_API.CustomValidation;
using CORWL_API.DbContext;
using CORWL_API.Extension;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;

namespace CORWL_API.Controllers.v1
{
    public class DesignationController : BaseApiController
    {
        private IUnitOfWork _uot;
        private IMapper _mapper;

        public DesignationController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }



        [HttpGet("GetAllDesignation")]

        public async Task<IActionResult> GetAllDesignation()
        {
            return Ok(await _uot.DesignationRepository.GetAllDesignation());
        }

        [HttpGet("GetDesignationById/{id}")]
        public async Task<IActionResult> GetDesignationById(int id)
        {
            return Ok(await _uot.DesignationRepository.GetDesignationById(id));
        }

        [HttpGet("GetDesignationDropdown")]
        public async Task<IActionResult> GetDesignationDropdown()
        {
            return Ok(await _uot.DesignationRepository.GetDesignationDropdown());
        }

        [HttpPost("AddDesignation")]
        public async Task<ActionResult> AddDesignation(DesignationDto designationDto)
        {
            var designationData = _mapper.Map<Designation>(designationDto);

            designationData.CreatedBy = int.Parse(User.GetUserId());

            var res = await _uot.DesignationRepository.AddDesignation(designationData);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Designation added successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("adding Designation"));
        }


        [HttpPut("UpdateDesignation")]
        public async Task<ActionResult> UpdateDesignation(DesignationDto designationDto)
        {
            designationDto.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.DesignationRepository.UpdateDesignation(designationDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete()) return Ok(new { Message = "Designation Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating Designation"));
        }
    }
}
