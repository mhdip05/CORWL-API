using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.FetchDTO;

namespace NMS_API_N.Model.IRepository
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
