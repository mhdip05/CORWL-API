﻿using AutoMapper;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>().ReverseMap();

            CreateMap<RoleDto, Role>().ReverseMap();

            CreateMap<CompanyDto, Company>().ReverseMap();

            CreateMap<AddressDto, Address>().ReverseMap();

            CreateMap<CountryDto, Country>().ReverseMap();

            CreateMap<CityDto, City>().ReverseMap();

            CreateMap<UserListDto, User>().ReverseMap();

            CreateMap<CurrencyDto, Currency>().ReverseMap();

            CreateMap<BankDto, Bank>().ReverseMap();

            CreateMap<BranchDto, Branch>().ReverseMap();

            CreateMap<DepartmentDto, Department>().ReverseMap();

            CreateMap<DesignationDto, Designation>().ReverseMap();

            CreateMap<EmployeeDocumentDto, EmployeeDocumentMaster>().ReverseMap();

            CreateMap<EmployeeBasicInfoDto, Employee>().ReverseMap();

            CreateMap<EmployeeDocumentMaseterDto, EmployeeDocumentMaster>().ReverseMap();

            CreateMap<UserInfoDto, User>().ReverseMap();
            CreateMap<UserDataDto, User>().ReverseMap();

            CreateMap<string, string>().ConvertUsing(new StringTrimmerProfile());

            //CreateMap<string, string>().ConvertUsing(new EmptyToNullConverter());
        }
    }
}
