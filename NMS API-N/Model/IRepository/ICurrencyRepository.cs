using NMS_API_N.Model.Entities;
using NMS_API_N.Model.FetchDTO;
using System.Collections;

namespace NMS_API_N.Model.IRepository
{
    public interface ICurrencyRepository
    {
        void AddCurrency(Currency currency);
        Task<IEnumerable<Currency>> GetAllCurrency();
        Task<IEnumerable<GetCurrenciesDto>> GetCurrencies();
        void UpdateCurrency(Currency currency);
        Task<Currency> GetCurrencyByName(string currencyName);
        Task<Currency> GetCurrencyById(int id);
        Task<int> DeleteCurrency(int id);
    }
}
