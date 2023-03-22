﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
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
