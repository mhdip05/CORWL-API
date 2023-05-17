using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Pagination;

namespace NMS_API_N.Model.IRepository
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
