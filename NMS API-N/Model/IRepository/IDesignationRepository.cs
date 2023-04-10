using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
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
