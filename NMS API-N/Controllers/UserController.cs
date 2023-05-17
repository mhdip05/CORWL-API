﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.CustomValidation;
using NMS_API_N.Extension;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
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

        [HttpGet("GetUserData/{employeeId}")]
        public async Task<ActionResult> GetUserData(int employeeId)
        {
            return Ok(await _uot.UserRepository.GetUserData(employeeId));
        }

        [HttpPost("SaveUserInfo")]
        public async Task<ActionResult> SaveUserInfo(UserInfoDto user)
        {
            var userInfo = _mapper.Map<User>(user);
            userInfo.CreatedBy = int.Parse(User.GetUserId());
            userInfo.CreatedDate = DateTime.Now;
            userInfo.PasswordHash = user.Password;

            var res = await _uot.UserRepository.SaveUserInfo(userInfo);

            if (res.Status == false) return BadRequest(res.Message);

            return Ok(new { Message = "User Saved Successfully", res.Status });

        }

        [HttpPut("UpdateUserInfo")]
        public async Task<ActionResult> UpdateUserInfo(UserDataDto userInfoDto)
        {
            userInfoDto.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.UserRepository.UpdateUserInfo(userInfoDto);

            if (res.Status == false) return BadRequest(res.Message);

            return Ok(new { Message = "User Info Updated Successfully", res.Data });


        }

        [HttpPut("UpdateUserPassword")]

        public async Task<ActionResult> UpdateUserPassword(UserPasswordDto userPasswordDto)
        {
            var res = await _uot.UserRepository.UpdateUserPassword(userPasswordDto);

            if (res.Status == false) return BadRequest(res.Message);

            return Ok(new Result { Status = res.Status, Message = "Password Upadted Successfully" });
        }
    }
}
