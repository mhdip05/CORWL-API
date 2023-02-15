using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Helper;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class BranchRepository : IBranchReposiroty
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BranchRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
#nullable disable
        public async Task<Branch> GetBranchByName(string branchName)
        {
            return await _context.Branches
                .Where(x => x.BranchName == branchName)
                .Select(x => new Branch { Id = x.Id, BranchName = x.BranchName })
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<Branch> GetBranchById(int id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<Result> AddBranch(Branch branch)
        {
            if (await GetBranchByName(branch.BranchName) != null)
                return new Result { Status = false, Message = ValidationMsg.Exist("Branch") };

            _context.Branches.Add(branch);

            return new Result { Status = true, Data = branch };
        }

        public async Task<Result> UpdateBranch(BranchDto branchDto)
        {
            var existBranch = await GetBranchById(branchDto.Id);

            if (existBranch == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            if (existBranch.BranchName.ToLower() != branchDto.BranchName.ToLower())
            {
                if (await GetBranchByName(branchDto.BranchName.ToLower()) != null)
                {
                    return new Result { Status = false, Message = ValidationMsg.Exist("This branch") };
                }
            }

            var branchData = _mapper.Map(branchDto, existBranch);

            branchData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

            _context.Attach(branchData);

            return new Result { Status = true, Data = branchDto };
        }

    }
}
