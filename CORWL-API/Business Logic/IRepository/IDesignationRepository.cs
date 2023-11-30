using CORWL_API.CustomValidation;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;

namespace CORWL_API.Business_Logic.IRepository
{
    public interface IDesignationRepository
    {
        Task<Result> AddDesignation(Designation designation);
        Task<Designation> GetDesignationByName(string designationName);
        Task<DesignationDto> GetDesignationById(int id);
        Task<IEnumerable<object>> GetDesignationDropdown();
        Task<Result> UpdateDesignation(DesignationDto designationDto);
        Task<IEnumerable<DesignationDto>> GetAllDesignation();
    }
}
