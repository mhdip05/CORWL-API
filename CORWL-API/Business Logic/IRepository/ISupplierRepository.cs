using CORWL_API.CustomValidation;
using CORWL_API.Model.Entities;

namespace CORWL_API.Business_Logic.IRepository
{
    public interface ISupplierRepository
    {
        Task<Result> SaveSupplierInfo(Supplier supplier);

        Task<bool> GetSupplierByCode(string code);
    }
}
