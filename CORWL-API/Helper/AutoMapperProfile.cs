using AutoMapper;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;

namespace CORWL_API.Helper
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

            CreateMap<EmployeeJobDetailsDto, EmployeeJobDetails>().ReverseMap();

            CreateMap<string, string>().ConvertUsing(source => StringTrimmerHelper.TrimString(source));

            //CreateMap<string, string>().ConvertUsing(new EmptyToNullConverter());
        }
    }
}
