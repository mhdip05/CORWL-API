using AutoMapper;
using CORWL_API.DbContext;
using CORWL_API.Model.Entities;
using CORWL_API.Model.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Countries.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Countries.Add(
                        new Country
                        {
                            CountryName = "country-" + i,
                            CountryAlias = "country alias-" + i
                        }
                  );
                }
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
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
