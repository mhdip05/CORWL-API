using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.Extension;
using NMS_API_N.Model.DTO;
using NMS_API_N.Pagination;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private IMapper _mapper;

        public UserController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserListDto>>> GetAllUsers([FromQuery] PaginationParams @params)
        {
            var users = await _uot.UserRepository.GetAllUsersAsync(@params);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            if (users == null) return Ok(new NoRecordFoundDto());

            return Ok(users);
        }


    }
}
