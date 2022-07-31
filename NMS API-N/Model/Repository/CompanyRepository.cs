using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class CompanyRepository :ICompanyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompanyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddCompany(Company conpany)
        {
            _context.Companies.Add(conpany);
        }

#nullable disable
        public async Task<Company> GetComanyByCompanyName(string companyName)
        {
            return await _context.Companies
                .Where(x => x.CompanyName == companyName)
                .Select(x => new Company { Id = x.Id, CompanyName = x.CompanyName })
                .SingleOrDefaultAsync();

            //var data = await (from company in _context.Companies
            //                  join users in _context.Users
            //                  on company.Id equals users.Id
            //                  select new CompanyDto

            //                  {


            //                  }).SingleOrDefaultAsync();

            //return data;
        }

        public async Task<Company> GetCompayByIdAsync(int Id)
        {          
            return await _context.Companies.FindAsync(Id);
        }

        public void UpdateCompany(Company company)
        {
            _context.Attach(company);
        }
    }
}
