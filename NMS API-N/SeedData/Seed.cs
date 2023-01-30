using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.Model.Entities;
using NMS_API_N.Extension;
using NMS_API_N.Unit_Of_Work;
using System.Text.Json;
using NMS_API_N.Model.DTO;

namespace NMS_API_N.SeedData
{
    public class Seed
    {
        private readonly IUnitOfWork _uot;

        public Seed()
        {
            _uot = (IUnitOfWork)ApplicationServiceExtension.serviceProvider.GetRequiredService(typeof(IUnitOfWork));
        }
        public static async Task SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var seed = new Seed();
            seed.SeedCompany();

            var rolse = new List<Role>()
            {
                new Role(){ Name = "admin", CreatedBy=1, CreatedDate=DateTime.Now},
                new Role(){ Name = "doctor", CreatedBy=1, CreatedDate=DateTime.Now},
                new Role(){ Name = "management", CreatedBy=1, CreatedDate=DateTime.Now}
            };

            foreach (var role in rolse)
            {
                await roleManager.CreateAsync(role);
            }

            await SeedAdmins(userManager);
            await SeedDoctors(userManager);
            await SeedManagement(userManager);

            Thread.Sleep(1500);

            seed.SeedCountry();
            seed.SeedCity();
            seed.SeedAddress();
        }

        private void SeedCompany()
        {
            var company = new Company
            {
                CompanyName = "neurogen",
                CompanyCode = "123456",
                CityId = 1,
                Address="Dhaka, Bangladesh",
                ZipCode = "12500",
                MobileNo = "123456789",
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
            };

            _uot.CompanyRepository.AddCompany(company);
            _uot.Complete();
        }

        private void SeedCountry()
        {
            var country = new Country
            {
                CountryName = "bangladesh",
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
            };

            _uot.CountryRepository.AddCountry(country);
            _uot.Complete();
        }

        private void SeedCity()
        {
            var city = new City
            {
                CityName = "dhaka",
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                CountryId = 1,
            };

            _uot.CityRepository.AddCity(city);
            _uot.Complete();
        }

        private void SeedAddress()
        {
            var address = new Address
            {
                AddressDescription = "New Elephant Road, Beside Cartoon Hall, 74/B/N",
                SourceId = 1,
                SourceType = "company",
                Phone = "+8801791468094",
                CityId = 1,
                
            };

            _uot.AddressRepository.AddAddress(address);
            _uot.Complete();
        }
        private static async Task SeedAdmins(UserManager<User> userManager)
        {

            var adminData = await File.ReadAllTextAsync("SeedFile/AdminRegister.json");

            var admins = JsonSerializer.Deserialize<List<User>>(adminData);

            foreach (var user in admins!)
            {
                user.UserName = user.UserName.ToLower();
                user.CreatedBy = 1;
                user.CreatedDate = DateTime.Now;

                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, "admin");
            }
        }

        private static async Task SeedDoctors(UserManager<User> userManager)
        {
            var userData = await File.ReadAllTextAsync("SeedFile/Doctor.json");

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach (var user in users!)
            {
                user.UserName = user.UserName.ToLower();
                user.CreatedBy = 1;
                user.CreatedDate = DateTime.Now;

                await userManager.CreateAsync(user, "123456");

                await userManager.AddToRoleAsync(user, "doctor");
            }
        }

        private static async Task SeedManagement(UserManager<User> userManager)
        {
            var managementData = await File.ReadAllTextAsync("SeedFile/Management.json");

            var management = JsonSerializer.Deserialize<List<User>>(managementData);

            foreach (var user in management!)
            {
                user.UserName = user.UserName.ToLower();
                user.CreatedBy = 1;
                user.CreatedDate = DateTime.Now;

                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, "management");
            }
        }

    }
}
