using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CORWL_API.CustomValidation;
using CORWL_API.DbContext;
using CORWL_API.Extension;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;

namespace CORWL_API.Controllers.v1
{
    [Authorize(Policy = "ManagementRole")]
    public class RoleController : BaseApiController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IUnitOfWork _uot;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper, DataContext context, UserManager<User> userManager, IUnitOfWork uot)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _uot = uot;
        }


        private async Task<bool> RoleExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
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

            checkRole.Name = roleDto.RoleName!.ToLower();
            checkRole.UpdatedBy = int.Parse(User.GetUserId());
            checkRole.LastUpdatedDate = DateTime.Now;

            var result = await _roleManager.UpdateAsync(checkRole);

            if (!result.Succeeded) BadRequest(result.Errors);

            return new Result { Message = "Role Updated Successfully", Data = checkRole };
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var query = from rol in _context.Roles
                        join usr in _context.Users on rol.CreatedBy equals usr.Id
                        into sbUsr
                        from subUsr in sbUsr.DefaultIfEmpty()

                        select new
                        {
                            rol.Id,
                            RoleName = rol.Name,
                            CreatedBy = subUsr.UserName,
                            rol.CreatedDate,
                            UpdatedBy = subUsr.UserName,
                            rol.LastUpdatedDate
                        };

            var a = await query.OrderBy(x => x.RoleName).ToListAsync();

            return Ok(a);
        }

        [HttpGet("GetUserRoles/{employeeId}")]
        public async Task<IActionResult> GetUserRoles(int employeeId)
        {
            var userData = await _context.Users.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (userData == null) return Ok(new Result { Status = false });

            var userMapdata = await (from usrRole in _context.UserRoles.Where(e => e.UserId == userData.Id)
                                     select new
                                     {
                                         roleId = usrRole.RoleId,
                                         value = true,
                                     }).ToListAsync();

            return Ok(userMapdata);
        }

        [HttpPost("MapUserRole")]
        public async Task<IActionResult> MapUserRole(UserRolesDto userRoles)
        {
#nullable disable
            var userData = await _context.Users.FirstOrDefaultAsync(e => e.EmployeeId == userRoles.EmployeeId);

            if (userData == null) return BadRequest("Please Add User Info First");

            foreach (var item in userRoles.RoleIds)
            {
                var getRole = await _context.Roles.FindAsync(item);

                var mappedRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userData.Id && x.RoleId == getRole.Id);

                if (mappedRole == null)
                {
                    await _userManager.AddToRoleAsync(userData, getRole.Name);
                }
            }

            return Ok(new Result { Status = true, Message = "Role Mapped Successfully" });
        }

        [HttpDelete("RemoveRoleMapping/{employeeId}/{roleId}")]
        public async Task<IActionResult> RemoveRoleMapping(int employeeId, int roleId)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (getUser == null) return BadRequest("No User Found");

            var getRole = await _context.Roles.FindAsync(roleId);

            if (getRole == null) return BadRequest("No Role Found");

            var removeRole = await _userManager.RemoveFromRoleAsync(getUser, getRole.Name);

            if (!removeRole.Succeeded) return BadRequest("Role did not remove");

            return Ok(new Result { Status = true, Message = "Role removed successfully" });
        }
    }
}
