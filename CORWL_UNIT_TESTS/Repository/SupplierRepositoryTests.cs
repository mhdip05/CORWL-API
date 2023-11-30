using AutoMapper;
using CORWL_API.Business_Logic.IRepository;
using CORWL_API.Business_Logic.Repository;
using CORWL_API.CustomValidation;
using CORWL_API.DbContext;
using CORWL_API.Model.Entities;
using CORWL_UNIT_TESTS.TEST_Utility;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORWL_UNIT_TESTS.Repository
{
    public class SupplierRepositoryTests
    {
#nullable disable

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SupplierRepositoryTests()
        {
            var mapperConfig = new MapperConfiguration(mc => { });
            _mapper = mapperConfig.CreateMapper();
            _context = DatabaseContextUtility.GetInMemoryDatabaseContext();
        }

        private async Task<DataContext> GetSupplierContext()
        {
            var supplierContext = DatabaseContextUtility.GetDataContext(_context, _context.Suppliers, 10, i => new Supplier
            {
                SupplierName = "supplier-" + i,
                SupplierCode = "sp-" + i,
                CountryId = i,
                CityId = i,
                AddressLineOne = "Address 1-" + i,
                PhoneOne = "012345678910" + i
            });

            return await supplierContext;
        }

        [Fact]
        public async void SupplierRepository_SaveSupplierInfo_ReturnTrue()
        {
            var repo = new SupplierRepository(_context, _mapper);
            var result = await repo.SaveSupplierInfo(new Supplier
            {
                SupplierCode = "sp-15899",
            });
            
            result.Status.Should().BeTrue();
        }

        [Fact]
        public async void SupplierRepository_GetSupplierByCode_ReturnTrue()
        {
            //Arrange
            var name = "sp-1";
            var supplierRepository = new SupplierRepository(await GetSupplierContext(), _mapper);

            //Action
            var result = await supplierRepository.GetSupplierByCode(name);

            //Assert
            result.Should().BeTrue();
        }



    }
}
