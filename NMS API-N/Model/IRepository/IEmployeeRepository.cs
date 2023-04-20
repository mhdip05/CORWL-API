using NMS_API_N.Model.DTO;

namespace NMS_API_N.Model.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<object>> GetEmployeeDropdown();

        void SaveDocumentInfo(EmployeeDocumentDto employeeDocument);
    }
}
