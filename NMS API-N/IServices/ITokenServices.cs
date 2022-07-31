using NMS_API_N.Model.Entities;

namespace NMS_API_N.IServices
{
    public interface ITokenServices
    {
        public Task<string> CreateToken(User user);
    }
}
