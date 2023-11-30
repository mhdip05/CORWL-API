using CORWL_API.CustomValidation;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Pagination;

namespace CORWL_API.Business_Logic.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<PageList<UserListDto>> GetAllUsersAsync(PaginationParams @params);
        Task<Result> SaveUserInfo(User user);
        Task<Result> UpdateUserInfo(UserDataDto userInfoDto);
        Task<Result> UpdateUserPassword(UserPasswordDto userPasswordDto);
        Task<UserInfoDto> GetUserData(int employeeId);
    }
}
