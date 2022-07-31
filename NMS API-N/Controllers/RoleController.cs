using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Extension;

namespace NMS_API_N.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost("addrole")]
        public async Task<ActionResult<RoleDto>> AddRole(RoleDto roleDto)
        {
            
            if (await RoleExist(roleDto.RoleName.ToLower().Trim())) return BadRequest("This role is already exist");

            var role = _mapper.Map<Role>(roleDto);

            role.Name = roleDto.RoleName!.ToLower();
            role.CreatedBy = int.Parse(User.GetUserId());

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded) BadRequest(result.Errors);

            return new RoleDto
            {
                Id = role.Id,
                RoleName = roleDto.RoleName,
            };
        }

        private async Task<bool> RoleExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
