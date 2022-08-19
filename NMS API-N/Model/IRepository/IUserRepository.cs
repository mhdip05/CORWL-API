using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
    }
}
