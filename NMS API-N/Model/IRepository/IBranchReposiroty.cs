using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface IBranchReposiroty
    {
        Task<Result> AddBranch(Branch branch);
        Task<Branch> GetBranchByName(string branchName);
        Task<Branch> GetBranchById(int id);
        Task<Result> UpdateBranch(BranchDto branchDto);
        Task<IEnumerable<BranchDto>> GetAllBranches();
    }
}
