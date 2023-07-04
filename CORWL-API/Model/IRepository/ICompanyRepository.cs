using CORWL_API.CustomValidation;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;

namespace CORWL_API.Model.IRepository
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
