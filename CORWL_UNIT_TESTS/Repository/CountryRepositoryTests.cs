using AutoMapper;
using CORWL_API.DbContext;
using CORWL_API.Model.Entities;
using CORWL_API.Business_Logic.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORWL_UNIT_TESTS.TEST_Utility;

namespace CORWL_UNIT_TESTS.Repository
{
    public class CountryRepositoryTests
    {
        private readonly IMapper _mapper;

        public CountryRepositoryTests()
        {
            var mapperConfig = new MapperConfiguration(mc => { });
            _mapper = mapperConfig.CreateMapper();
        }
        private static async Task<DataContext> GetDatabaseContext()
        {
            var mdc = DatabaseContextUtility.GetInMemoryDatabaseContext();

            var countryContext = DatabaseContextUtility.GetDataContext(mdc, mdc.Countries, 10, i => new Country
            {
                CountryName = "country-" + i,
                CountryAlias = "country alias-" + i
            });

            return await countryContext;
        }


        [Fact]
        public async void CountryRepository_GetCountryByName_ReturnsCountry()
        {
            //Arrange
            var name = "country-1";
            var dbContext = await GetDatabaseContext();

            var countryRepository = new CountryRepository(dbContext, _mapper);

            //Action
            var result = await countryRepository.GetCountryByName(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Country>();
        }
    }
}
