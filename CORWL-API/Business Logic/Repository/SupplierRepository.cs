using AutoMapper;
using CORWL_API.DbContext;
using CORWL_API.Business_Logic.IRepository;
using CORWL_API.CustomValidation;
using CORWL_API.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CORWL_API.Business_Logic.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private DataContext _context;
        private IMapper _mapper;

        public SupplierRepository(DataContext context, IMapper mappper)
        {
            _context = context;
            _mapper = mappper;
        }

        public async Task<bool> GetSupplierByCode(string code)
        {
            return await _context.Suppliers.AnyAsync(e => e.SupplierCode == code);
        }

        public async Task<Result> SaveSupplierInfo(Supplier supplier)
        {
            if (await GetSupplierByCode(supplier.SupplierCode) == false) 
                return new Result { Status = false, Message = ValidationMsg.Exist("Supplier code exist") };

            _context.Suppliers.Add(supplier);

            return new Result { Status = true, Message = ValidationMsg.Saved()};
        }


    }
}
