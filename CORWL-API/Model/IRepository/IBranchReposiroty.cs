using CORWL_API.CustomValidation;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;

namespace CORWL_API.Model.IRepository
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
