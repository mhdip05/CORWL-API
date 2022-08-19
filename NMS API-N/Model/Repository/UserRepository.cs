using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetUserByUsername(string username)
        {
#nullable disable
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
        }
    }
}
