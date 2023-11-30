using AutoMapper;
using CORWL_API.Business_Logic.IRepository;
using CORWL_API.Controllers.v1;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;
using System.Security.Claims;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CORWL_UNIT_TESTS.Controller
{
    public class SupplierControllerTests
    {
        private readonly IUnitOfWork _fakeUot;
        private readonly IMapper _fakeMapper;
        private readonly ISupplierRepository _fakeSupplierRepository;
        private readonly SupplierController _supplierController;

        public SupplierControllerTests()
        {
            _fakeUot = A.Fake<IUnitOfWork>();
            _fakeMapper = A.Fake<IMapper>();
            _fakeSupplierRepository = A.Fake<ISupplierRepository>();
            _supplierController = new SupplierController(_fakeUot, _fakeMapper);
        }

        [Fact]
        public async Task SupplierController_SaveSupplier_ReturnsOkResult()
        {
            var supplierCode = "Sp-2013";
            var supplierDto = new SupplierDto { SupplierCode = supplierCode };
            var supplier = new Supplier { SupplierCode = supplierCode };

            var a =A.CallTo(() => _fakeMapper.Map<Supplier>(A<SupplierDto>._)).Returns(supplier);
            var b = A.CallTo(() => _fakeUot.SupplierRepository).Returns(_fakeSupplierRepository);
            var c =A.CallTo(()=> _fakeUot.Complete()).Returns(true);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
                 new Claim(ClaimTypes.NameIdentifier, "1")
      }, "mock"));

            _supplierController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _supplierController.SaveSupplier(supplierDto);

            result.Should().BeOfType<OkObjectResult>();

        }
    }
}
