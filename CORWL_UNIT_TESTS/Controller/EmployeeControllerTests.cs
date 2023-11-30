using AutoMapper;
using CORWL_API.Controllers.v1;
using CORWL_API.CustomValidation;
using CORWL_API.Business_Logic.IRepository;
using CORWL_API.Unit_Of_Work;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORWL_UNIT_TESTS.Controller
{
    public class EmployeeControllerTests
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _uot = A.Fake<IUnitOfWork>();
            _mapper = A.Fake<IMapper>();
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _employeeController = new EmployeeController(_uot, _mapper);
        }

        [Fact]
        public void EmployeeController_GetEmployeeBasicInfo_ReturnSuccess()
        {
            //Arrange 
            var id = 1;
            var employee = A.Fake<Result>();
            A.CallTo(() => _employeeRepository.GetEmployeeBasicInfo(id)).Returns(employee);

            //Act
            var result = _employeeController.GetEmployeeBasicInfo(id);

            //Assertion
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
