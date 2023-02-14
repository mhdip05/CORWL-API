using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Controllers
{
    [Authorize(Policy = "ManagementRole")]
    public class RoleController : BaseApiController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper, DataContext context)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
        }

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

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var query = from rol in _context.Roles
                        select new
                        {
                            rol.Id,
                            rol.Name,
                        };

            var a = await query.ToListAsync();

            return Ok(a);
        }

        private async Task<bool> RoleExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
