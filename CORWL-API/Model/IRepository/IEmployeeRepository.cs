using CORWL_API.CustomValidation;
using CORWL_API.IServices;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CORWL_API.Model.IRepository
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
        Task<EmployeeJobDetailsDto> GetEmployeeJobDetails(int employeeId);
        Task<Result> SaveEmployeeJobDetails(EmployeeJobDetails employeeJobDetails);
        Task<Result> UpdateEmployeeJobDetails(EmployeeJobDetailsDto employeeJobDetails);
        Task<PageList<EmployeeBasicInfoDto>> GetAllEmployee(PaginationParams @params);
        Task<bool> DeleteEmployeeDoc(int fileId, int empId);
        Task<bool> DeleteEmployeeDocsFromAzure(int FileId);


    }
}
