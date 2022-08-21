using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Extension;
using NMS_API_N.IServices;



namespace NMS_API_N.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly ITokenServices _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(ITokenServices tokenService,
                              IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userValidation = await UniqueUserValidation(registerDto);

            if (userValidation != "") return BadRequest(userValidation);

            var user = _mapper.Map<User>(registerDto);

            user.UserName = registerDto.UserName!.ToLower();
            user.Email = registerDto.Email!.ToLower();
            user.CreatedBy = int.Parse(User.GetUserId());

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) BadRequest(result.Errors);

            return new UserDto
            {
                UserId = user.Id,
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                EmployeeId = user.EmployeeId,
            };

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username!.ToLower());

            if (user == null) return BadRequest("The username is entered is not correct");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return BadRequest("The password is entered is not correct");

            return new UserDto
            {
                UserId = user.Id,
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                EmployeeId = user.EmployeeId,
            };
        }

        private async Task<User> GetUserByUserName(string username)
        {
#nullable disable
            return await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        private async Task<string> UniqueUserValidation(RegisterDto registerDto)
        {

            if (await UserExist(registerDto.UserName!))
                return "Username is taken";
            else if (await EmailExist(registerDto.Email!))
                return "Email is taken";
            else if (await PhoneExist(registerDto.PhoneNumber!))
                return "Phone number is taken";

            return "";
        }
        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username);
        }

        private async Task<bool> EmailExist(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);
        }

        private async Task<bool> PhoneExist(string phoneNumber)
        {
            return await _userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
