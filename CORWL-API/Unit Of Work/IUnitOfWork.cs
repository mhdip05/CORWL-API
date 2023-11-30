﻿using CORWL_API.Business_Logic.IRepository;

namespace CORWL_API.Unit_Of_Work
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        IAddressRepository AddressRepository { get; }
        ICountryRepository CountryRepository { get; }
        ICityRepository CityRepository { get; }
        IUserRepository UserRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        IBankRepository BankRepository { get; }
        IBranchReposiroty BranchReposiroty { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IDesignationRepository DesignationRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
