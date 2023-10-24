using AutoMapper;
using CORWL_API.Business_Logic.Repository;
using CORWL_API.DbContext;
using CORWL_API.Model.Entities;
using CORWL_UNIT_TESTS.TEST_Utility;
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
        private readonly IMapper _mapper;

        public SupplierRepositoryTests()
        {
            var mapperConfig = new MapperConfiguration(mc => { });
            _mapper = mapperConfig.CreateMapper();
        }

        private static async Task<DataContext> GetSupplierContext()
        {
            var t = DatabaseContextUtility.GetInMemoryDatabaseContext();

            var supplierContext = DatabaseContextUtility.GetDataContext(t, t.Suppliers, 10, i => new Supplier
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
        public async void SupplierRepository_GetSupplierByCode_ReturnSupplier()
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
