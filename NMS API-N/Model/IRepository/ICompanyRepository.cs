using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface ICompanyRepository
    {
        Task<Result> AddCompany(Company company);
        Task<CompanyDto> GetCompayByIdAsync(int id);
        Task<Result> UpdateCompany(CompanyDto companyDto);
        Task<Company> FindCompanyByName(string companyName);
        Task<Company> FindCompanyById(int id);
        Task<IEnumerable<CompanyDto>> GetAllCompanies();
        Task<IEnumerable<object>> GetCompanyDropdown();
        Task<Result> DeleteCompany(int id);
    }
}
