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
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SupplierRepository(DataContext context, IMapper mappper)
        {
            _context = context;
            _mapper = mappper;
        }

        public async Task<bool> GetSupplierByCode(string code)
        {
            var a = await _context.Suppliers.AnyAsync(e => e.SupplierCode == code);
            return a;
        }

        public async Task<Result> SaveSupplierInfo(Supplier supplier)
        {
            if (await GetSupplierByCode(supplier.SupplierCode) == true) 
                return new Result { Status = false, Message = ValidationMsg.Exist("Supplier code exist") };

            _context.Suppliers.Add(supplier);

            return new Result { Status = true, Message = ValidationMsg.Saved()};
        }
    }
}
