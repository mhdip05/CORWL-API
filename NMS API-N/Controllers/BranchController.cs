using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.CustomValidation;
using NMS_API_N.Extension;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
{
    [Authorize("ManagementRole")]
    public class BranchController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public BranchController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpGet("GetAllBranch")]
        public async Task<IActionResult> GetAllBranche()
        {
            return Ok(await _uot.BranchReposiroty.GetAllBranches());
        }


        [HttpGet("GetBranchById/{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            return Ok(await _uot.BranchReposiroty.GetBranchById(id));

        }

        [HttpPost("AddBranch")]
        public async Task<ActionResult> AddCompany(BranchDto branchDto)
        {
            var branchData = _mapper.Map<Branch>(branchDto);

            branchData.CreatedBy = int.Parse(User.GetUserId());

            var res = await _uot.BranchReposiroty.AddBranch(branchData);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new { Message = "Branch added successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("adding branch"));
        }

        [HttpPut("UpdateBranch")]
        public async Task<ActionResult> UpdateBranch(BranchDto branchDto)
        {
            branchDto.UpdatedBy= int.Parse(User.GetUserId());

            var res = await _uot.BranchReposiroty.UpdateBranch(branchDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete()) return Ok(new { Message="Branch Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating branch"));
        }
    }
}
