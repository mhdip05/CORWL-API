using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Model.FetchDTO;

namespace CORWL_API.Model.IRepository
{
    public interface ICurrencyRepository
    {
        void AddCurrency(Currency currency);
        Task<IEnumerable<CurrencyDto>> GetAllCurrency();
        Task<IEnumerable<object>> GetCurrencyDropdown();
        void UpdateCurrency(Currency currency);
        Task<Currency> GetCurrencyByName(string currencyName);
        Task<Currency> GetCurrencyById(int id);
        Task<int> DeleteCurrency(int id);
    }
}
