using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Pagination;

namespace NMS_API_N.Model.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<object>> GetEmployeeDropdown();
        Task<Result> SaveEmployeeBasicInfo(Employee employee);
        Task<Result> SaveDocument(EmployeeDocumentMaster employeeDocument, List<IFormFile> fileCollection);
        Task<Result> UpdateEmployeeDocumentMaster(EmployeeDocumentMaseterDto employeeDocument);
        Task<object> GetDocumentInfoByEmployee(int employeeId);
        Task<Result> GetEmployeeBasicInfo(int employeeId);
        Task<Result> UpdateEmployeeBasicInfo(EmployeeBasicInfoDto employeeBasicInfoDto);  
        Task<PageList<EmployeeBasicInfoDto>> GetAllEmployee(PaginationParams @params);
        Task<bool> DeleteEmployeeDoc(int fileId, int empId);
       
    }
}
