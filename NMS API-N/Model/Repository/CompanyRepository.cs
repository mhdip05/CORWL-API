﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Helper;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompanyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> AddCompany(Company company)
        {
            if (await FindCompanyByName(company.CompanyName) != null)
                return new Result { Status = false, Message = ValidationMsg.Exist("Company") };

            _context.Companies.Add(company);

            return new Result { Status = true, Data = company };
        }

        private IQueryable<CompanyDto> FetchAllComnpay()
        {
            return from com in _context.Companies
                   join cty in _context.Cities on com.CityId equals cty.Id
                   join con in _context.Countries on cty.CountryId equals con.Id
                   join lc in _context.Currencies on com.LocalCurrencyId equals lc.Id
                   into slc
                   from sublc in slc.DefaultIfEmpty()
                   join ic in _context.Currencies on com.InterNationalCurrencyId equals ic.Id
                   into sic
                   from subic in sic.DefaultIfEmpty()

                   select new CompanyDto
                   {
                       Id = com.Id,
                       CompanyName = com.CompanyName.ToUpper(),
                       CompanyCode = com.CompanyCode,
                       CountryId = cty.CountryId,
                       CountryName = con.CountryName.ToUpper(),
                       CityId = cty.Id,
                       CityName = cty.CityName.ToUpper(),
                       MobileNo = com.MobileNo,
                       PhoneNo = com.PhoneNo,
                       Email = com.Email,
                       Web = com.Web,
                       ZipCode = com.ZipCode,
                       Address = com.Address,
                       ConversionRate = com.ConversionRate,
                       CreatedBy = com.CreatedBy,
                       CreatedDate = com.CreatedDate,
                       UpdatedBy = com.UpdatedBy,
                       LastUpdatedDate = com.LastUpdatedDate ?? null,
                       LocalCurrencyId = com.LocalCurrencyId,
                       LocalCurrencyName = sublc.CurrencyName,
                       InterNationalCurrencyId = com.InterNationalCurrencyId,
                       InterNationalCurrencyName = subic.CurrencyName,
                       IsActive = com.IsActive,
                       IsParentCompany = com.IsParentCompany,
                   };

        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            return await FetchAllComnpay().AsNoTracking().ToListAsync();
        }
#nullable disable
        public async Task<Company> FindCompanyByName(string companyName)
        {
            return await _context.Companies
                .Where(x => x.CompanyName.ToLower() == companyName.ToLower())
                .Select(x => new Company { Id = x.Id, CompanyName = x.CompanyName })
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<Company> FindCompanyById(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<CompanyDto> GetCompayByIdAsync(int id)
        {
            return await FetchAllComnpay().Where(e => e.Id == id).AsNoTracking().FirstAsync();
        }

        public async Task<Result> UpdateCompany(CompanyDto companyDto)
        {
            var existCompany = await FindCompanyById(companyDto.Id);

            if (existCompany == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            if (existCompany.CompanyName.ToLower() != companyDto.CompanyName.ToLower())
            {
                if (await FindCompanyByName(companyDto.CompanyName.ToLower()) != null)
                {
                    return new Result { Status = false, Message = ValidationMsg.Exist("This company") };
                }
            }

            var companyData = _mapper.Map(companyDto, existCompany);

            companyData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());
            companyData.UpdatedCount += 1;

            _context.Attach(companyData);

            return new Result { Status = true, Data = companyDto };
        }

        public async Task<Result> DeleteCompany(int id)
        {
            var companyData = await _context.Companies.FindAsync(id);

            if (companyData == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            _context.Companies.Remove(companyData);

            return new Result { Status = true };

        }


    }
}
