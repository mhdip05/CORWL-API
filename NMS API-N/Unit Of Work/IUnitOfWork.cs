using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Unit_Of_Work
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
        Task<bool> Complete();
        bool HasChanges();
    }
}
