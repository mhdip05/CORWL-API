using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CORWL_API.CustomValidation;
using CORWL_API.Extension;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;

namespace CORWL_API.Controllers.v1
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

        [HttpGet("GetBranchDropdown")]
        public async Task<IActionResult> GetBranchDropdown()
        {
            return Ok(await _uot.BranchReposiroty.GetBranchDropdown());

        }

        [HttpPost("AddBranch")]
        public async Task<ActionResult> AddBranch(BranchDto branchDto)
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
            branchDto.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.BranchReposiroty.UpdateBranch(branchDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete()) return Ok(new { Message = "Branch Updated Successfully", res.Data });

            return BadRequest(ValidationMsg.SomethingWrong("updating branch"));
        }
    }
}
