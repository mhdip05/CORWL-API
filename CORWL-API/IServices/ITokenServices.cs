using CORWL_API.Model.Entities;

namespace CORWL_API.IServices
{
    public interface ITokenServices
    {
        public Task<string> CreateToken(User user);
    }
}
