using CORWL_API.CustomValidation;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;

namespace CORWL_API.Business_Logic.IRepository
{
    public interface IDepartmentRepository
    {
        Task<Result> AddDepartment(Department department);
        Task<Department> GetDepartmentByName(string departmentName);
        Task<DepartmentDto> GetDepartmentById(int id);
        Task<Result> UpdateDepartment(DepartmentDto departmentDto);
        Task<IEnumerable<DepartmentDto>> GetAllDepartment();
        Task<IEnumerable<object>> GetDepartmentDropdown();
    }
}
