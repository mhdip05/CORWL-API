using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
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
        public async Task<ActionResult<Result>> AddRole(RoleDto roleDto)
        {

            if (await RoleExist(roleDto.RoleName.ToLower().Trim())) return BadRequest("This role is already exist");

            var role = _mapper.Map<Role>(roleDto);

            role.Name = roleDto.RoleName!.ToLower();
            role.CreatedBy = int.Parse(User.GetUserId());

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded) BadRequest(result.Errors);

            var roleData = new RoleDto
            {
                Id = role.Id,
                RoleName = roleDto.RoleName,
            };

            return new Result { Message = "Role Added Successfully", Data = roleData };
        }

        [HttpPut("UpdateRole")]
        public async Task<ActionResult<Result>> UpdateRole(RoleDto roleDto)
        {
            var checkRole = _context.Roles.Find(roleDto.Id);

            if (checkRole == null) return BadRequest("Role Doesn't Exist");

            checkRole.Name= roleDto.RoleName!.ToLower();
            checkRole.UpdatedBy= int.Parse(User.GetUserId());
            checkRole.LastUpdatedDate= DateTime.Now;

            var result = await _roleManager.UpdateAsync(checkRole);

            if (!result.Succeeded) BadRequest(result.Errors);

            return new Result { Message = "Role Updated Successfully", Data = checkRole };
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var query = from rol in _context.Roles
                        join usr in _context.Users on rol.CreatedBy equals usr.Id
                        into sbUsr from subUsr in sbUsr.DefaultIfEmpty()

                        select new
                        {
                            rol.Id,
                            RoleName = rol.Name,
                            CreatedBy = subUsr.UserName,
                            rol.CreatedDate,
                            UpdatedBy = subUsr.UserName,
                            rol.LastUpdatedDate
                        };

            var a = await query.OrderByDescending(x=>x.Id).ToListAsync();

            return Ok(a);
        }

        private async Task<bool> RoleExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
