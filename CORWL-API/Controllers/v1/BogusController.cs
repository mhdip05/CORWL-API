using Azure.Storage.Blobs.Models;
using Bogus;
using CORWL_API.DbContext;
using CORWL_API.IServices;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CORWL_API.Controllers.v1
{
    public class BogusController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _uot;

        public BogusController(DataContext context, IUnitOfWork uot)
        {
            _context = context;
            _uot = uot;
        }
#nullable disable
        [HttpPost("AddBogusCountry")]
        public ActionResult AddBogusCountry()
        {

            var countryFaker = new Faker<Country>()
                .RuleFor(c => c.CountryName, f => f.Address.Country())
                .RuleFor(c => c.CountryAlias, (f, c) => c.CountryName[..2].ToUpper())
                .RuleFor(c => c.TelephoneCode, f => f.Address.Random.Int(1, 99).ToString() + f.UniqueIndex);

            var country = Enumerable.Range(1, 20)
                .Select(_ => countryFaker.Generate())
                .ToList();

            // _context.Countries.AddRange(country);

            //  await _context.SaveChangesAsync();

            return Ok(country);
        }

        string GetRandomString(string type)
        {
            Random random = new();
            string[] bloodGroups = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            string[] maritalStatus = { "Married", "Single", "Divorced" };
            string[] idType = { "driving license", "voter id", "registration number" };

            switch (type)
            {

                case "bloodGroup":
                    int bgroup = random.Next(0, bloodGroups.Length);
                    return bloodGroups[bgroup];

                case "maritalStatus":
                    int gnd = random.Next(0, maritalStatus.Length);
                    return maritalStatus[gnd];

                case "idType":
                    int idt = random.Next(0, idType.Length);
                    return idType[idt];

                default:
                    return null;
            }
        }

        string GenerateEmailOrUserName(string firstName, string lastName, string type)
        {
            return type switch
            {
                "username" => $"{firstName.ToLower()}.{lastName.ToLower()}",
                "email" => $"{firstName.ToLower()}.{lastName.ToLower()}@gmail.com",
                _ => null,
            };
        }

        [HttpPost("AddBogusEmployeeAndUser")]
        public async Task<ActionResult> AddEmployeeAndUser()
        {
            var employeeFaker = new Faker<Employee>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Dob, f => f.Date.Between(new DateTime(1980, 1, 1), new DateTime(2008, 12, 12)))
                .RuleFor(c => c.Gender, f => f.Person.Gender.ToString())
                .RuleFor(c => c.BloodGroup, f => GetRandomString("bloodGroup"))
                .RuleFor(c => c.MaritalStatus, f => GetRandomString("maritalStatus"))
                .RuleFor(c => c.Status, f => f.Random.Replace("active"))
                .RuleFor(c => c.IdType, f => GetRandomString("idType"))
                .RuleFor(c => c.IdNo, f => f.Random.Int(32657898, 99999999).ToString())
                .RuleFor(c => c.CompanyId, f => f.Random.Number(2, 2))
                .RuleFor(c => c.CreatedBy, f => f.Random.Number(1, 1));


            var employee = Enumerable.Range(1, 50)
            .Select(_ => employeeFaker.Generate())
            .ToList();

            _context.Employees.AddRange(employee);

            await _context.SaveChangesAsync();

            var userList = new HashSet<User>(employee.Select(item =>
            {
                var userFaker = new Faker<User>()
                .RuleFor(c => c.EmployeeId, f => f.Random.Number(3, 3))
                .RuleFor(c => c.UserName, f => GenerateEmailOrUserName(item.FirstName, item.LastName, "username"))
                .RuleFor(c => c.PasswordHash, f => f.Random.Replace("123456"))
                .RuleFor(c => c.IsActive, f => f.Random.Bool(0.3f))
                .RuleFor(c => c.CreatedBy, f => f.Random.Number(1, 1))
                .RuleFor(c => c.Email, f => GenerateEmailOrUserName(item.FirstName, item.LastName, "email"));

                return userFaker.Generate();
            }));

            return Ok(new { employeeInfo = employee, userInfo = userList });
        }

        [HttpPost("AddUserDataFormEmployee")]

        public async Task<ActionResult> AddUserDataFromEmployee([FromServices] UserManager<User> userManager)
        {
            var employee = _context.Employees.ToList();
            var users = new HashSet<User>();

            foreach (var item in employee)
            {
                var user = new User
                {
                    UserName = $"{item.FirstName}.{item.LastName}",
                    Email = $"{item.FirstName}.{item.LastName}@gmail.com",
                    PasswordHash = "123456",
                    CreatedBy = 2,
                    EmployeeId = item.Id
                };

                //users.Add(user);
                await userManager.CreateAsync(user, user.PasswordHash);
            }

            return Ok(users);
        }

        [HttpPost("UploadFileToAzureStorage")]
        public async Task<IActionResult> UploadFileToAzureStorage([FromServices] IAzureBlob azureBlob,List<IFormFile> files, string directory, string subdirectory = "", string folderName = "")
        {
            var data = await azureBlob.UploadFileToAzureStorage(files, directory, subdirectory, folderName);

             return Ok(data);
        }

    }
}
