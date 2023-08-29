using AutoMapper;
using CORWL_API.Controllers.v1;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Model.IRepository;
using CORWL_API.Unit_Of_Work;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CORWL_UNIT_TESTS.Controller
{
    public class CountryControllerTests
    {
        private readonly IUnitOfWork _fakeUot;
        private readonly IMapper _fakeMapper;
        private readonly CountryController _countryController;
        private readonly ICountryRepository _fakeCountryRepository;
        public CountryControllerTests()
        {
            _fakeUot = A.Fake<IUnitOfWork>();
            _fakeMapper = A.Fake<IMapper>();
            _fakeCountryRepository = A.Fake<ICountryRepository>();
            _countryController = new CountryController(_fakeUot, _fakeMapper);
        }
#nullable disable
        [Fact]
        public void CountryController_GetAllCountry_ReturnOk()
        {
            //Arrange
            var country = A.Fake<ICollection<CountryDto>>();
            var countryList = A.Fake<List<CountryDto>>();
            A.CallTo(() => _fakeCountryRepository.GetAllCountry()).Returns(country);

            //action
            var result = _countryController.GetAllCountries();

            //assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Fact]
        public async Task AddCountry_ValidCountry_ReturnsOkResult()
        {

            // Arrange
            var countryName = "Test Country";
            var countryDto = new CountryDto { CountryName = countryName };
            var country = new Country { CountryName = countryName };

            A.CallTo(() => _fakeMapper.Map<Country>(A<CountryDto>._)).Returns(country);
            A.CallTo(() => _fakeUot.CountryRepository).Returns(_fakeCountryRepository);
            A.CallTo(() => _fakeCountryRepository.GetCountryByName(countryName.ToLower())).Returns((Country)null);
            A.CallTo(() => _fakeUot.Complete()).Returns(true);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            _countryController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await _countryController.AddCountry(countryDto);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
