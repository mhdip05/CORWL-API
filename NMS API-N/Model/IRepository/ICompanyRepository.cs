using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface ICompanyRepository
    {
        void AddCompany(Company conpany);
        Task<Company> GetCompayByIdAsync(int id);
        void UpdateCompany(Company conpany);
        Task<Company> GetComanyByCompanyName(string companyName);

    }
}
