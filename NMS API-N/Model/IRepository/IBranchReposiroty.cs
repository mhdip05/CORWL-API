using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface IBranchReposiroty
    {
        Task<Result> AddBranch(Branch branch);
        Task<Branch> GetBranchByName(string branchName);
        Task<BranchDto> GetBranchById(int id);
        Task<IEnumerable<object>> GetBranchDropdown();
        Task<Result> UpdateBranch(BranchDto branchDto);
        Task<IEnumerable<BranchDto>> GetAllBranches();
    }
}
