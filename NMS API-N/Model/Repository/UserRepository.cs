using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;
using NMS_API_N.Pagination;

namespace NMS_API_N.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = (UserManager<User>)IdentityServiceExtension.serviceProvider.GetRequiredService(typeof(UserManager<User>));
        }

        private async Task<Result> CheckUser(User user)
        {
            var checkUserName = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (checkUserName != null) return new Result { Status = false, Message = "Username is exist" };

            var checkEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (checkEmail != null) return new Result { Status = false, Message = "Email id is exist" };

            var checkPhone = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == user.PhoneNumber);

            if (checkPhone != null) return new Result { Status = false, Message = "Phone no is exist" };

            return new Result { Status = true };
        }

        public async Task<PageList<UserListDto>> GetAllUsersAsync(PaginationParams @params)
        {
            var query = _context.Users
                .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PageList<UserListDto>.CreateAsynch(query, @params.PageNumber, @params.PageSize);

        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
#nullable disable
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        public async Task<Result> SaveUserInfo(User user)
        {
            var checkUser = await CheckUser(user);

            if (checkUser.Status == false) return new Result { Status = false, Message = checkUser.Message };

            var userData = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!userData.Succeeded) return new Result { Status = false, Message = "User data did not save" };

            return new Result { Status = true, Data = user };
        }

        public async Task<Result> UpdateUserInfo(UserDataDto userInfoDto)
        {
            var checkUser = await _userManager.FindByIdAsync(userInfoDto.Id.ToString());

            if (checkUser == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            var userData = _mapper.Map(userInfoDto, checkUser);
            userData.LastUpdatedDate = DateTime.UtcNow;

            var updateUser = await _userManager.UpdateAsync(userData);

            if (!updateUser.Succeeded) return new Result { Status = false, Message = ValidationMsg.SomethingWrong() };

            return new Result { Status = true };
        }

        public async Task<Result> UpdateUserPassword(UserPasswordDto userPasswordDto)
        {
            var userData = await _userManager.FindByIdAsync(userPasswordDto.Id.ToString());

            if (userData == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            var removePassword = await _userManager.RemovePasswordAsync(userData);

            if (!removePassword.Succeeded) return new Result { Status = false, Message = ValidationMsg.SomethingWrong() };

            var result = await _userManager.AddPasswordAsync(userData, userPasswordDto.Password);

            if (result.Succeeded) return new Result { Status = true, Message = "Password Updated Successfully" };

            return new Result { Status = false, Message = ValidationMsg.SomethingWrong() };
        }

        public async Task<UserInfoDto> GetUserData(int employeeId)
        {
            return await (from usr in _context.Users.Where(e => e.EmployeeId == employeeId)
                          select new UserInfoDto
                          {
                              Id = usr.Id,
                              Username = usr.UserName,
                              Email = usr.Email,
                              PhoneNumber = usr.PhoneNumber,
                              EmployeeId = usr.EmployeeId,

                          }).FirstOrDefaultAsync();
        }

    }
}
