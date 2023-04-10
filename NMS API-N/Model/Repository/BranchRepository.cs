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

        private IQueryable<BranchDto> FetchAllBrances()
        {
            return from br in _context.Branches
                   join cty in _context.Cities on br.CityId equals cty.Id
                   join con in _context.Countries on cty.CountryId equals con.Id
                   join com in _context.Companies on br.CompanyId equals com.Id

                   join brap in _context.Employees on br.BranchAttentionPersonId equals brap.Id
                   into sbrap
                   from subbrap in sbrap.DefaultIfEmpty()

                   join brai in _context.Employees on br.BranchInchargeId equals brai.Id
                   into sbrai
                   from subbrai in sbrai.DefaultIfEmpty()

                   select new BranchDto
                   {
                       Id = br.Id,
                       BranchName = br.BranchName,
                       BranchCode = br.BranchCode,
                       CityId = cty.Id,
                       CityName = cty.CityName.ToUpper(),
                       CountryId = con.Id,
                       CountryName = con.CountryName.ToUpper(),
                       BranchAttentionPersonId = subbrap.Id,
                       BranchAttentionPersonName = string.IsNullOrWhiteSpace(subbrap.FirstName + " " + subbrap.LastName) ? null : subbrap.FirstName + " " + subbrap.LastName,
                       BranchInchargeId = subbrai.Id,
                       BranchInchargeName = string.IsNullOrWhiteSpace(subbrai.FirstName + " " + subbrai.LastName) ? null : subbrai.FirstName + " " + subbrai.LastName,
                       CompanyId = com.Id,
                       CompanyName = com.CompanyName,
                       BranchType = br.BranchType,
                       Address = br.Address,
                       ZipCode = br.ZipCode,
                       Mobile = br.Mobile,
                       Email = br.Email,
                       Phone = br.Phone,
                       Web = br.Web,
                       CreatedBy = br.CreatedBy,
                       CreatedDate = br.CreatedDate,
                       UpdatedBy = br.UpdatedBy,
                       LastUpdatedDate = br.LastUpdatedDate,
                       IsActive = br.IsActive,
                   };
        }
        public async Task<IEnumerable<BranchDto>> GetAllBranches()
        {
            return await FetchAllBrances().AsNoTracking().ToListAsync();
        }
        public async Task<Branch> GetBranchByName(string branchName)
        {
            return await _context.Branches
                .Where(x => x.BranchName == branchName)
                .Select(x => new Branch { Id = x.Id, BranchName = x.BranchName })
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<object>> GetBranchDropdown()
        {
            return await _context.Branches
              .Select(x => new { BranchId = x.Id, x.BranchName })
              .AsNoTracking()
              .ToListAsync();
        }

        public async Task<BranchDto> GetBranchById(int id)
        {
            return await FetchAllBrances().Where(e => e.Id == id).AsNoTracking().FirstAsync();
        }

        public async Task<Branch> FindBranchById(int id)
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
            var existBranch = await FindBranchById(branchDto.Id);

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
