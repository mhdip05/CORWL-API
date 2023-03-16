﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Helper;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IQueryable<DepartmentDto> FetchAllDepartment()
        {
            return from dp in _context.Departments
                   join em in _context.Employees on dp.DepartmentHeadId equals em.Id
                   join usr in _context.Users on dp.CreatedBy equals usr.Id

                   select new DepartmentDto
                   {
                       Id = dp.Id,
                       DepartmentName = dp.DepartmentName,
                       Abbreviation= dp.Abbreviation,
                       DepartmentCode = dp.DepartmentCode,
                       DepartmentAddress = dp.DepartmentAddress,
                       DepartmentHeadId = dp.DepartmentHeadId,
                       DepartmentHeadName = string.IsNullOrWhiteSpace(em.FirstName + " " + em.LastName) ? null : em.FirstName + " " + em.LastName,
                       CreatedByName = usr.UserName,
                   };
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartment()
        {
            return await FetchAllDepartment().AsNoTracking().ToListAsync();
        }

        public async Task<DepartmentDto> GetDepartmentById(int id)
        {
            return await FetchAllDepartment().Where(e => e.Id == id).AsNoTracking().FirstAsync();
        }
#nullable disable
        public async Task<Department> GetDepartmentByName(string departmentName)
        {
            return await _context.Departments
                .Where(x => x.DepartmentName == departmentName)
                .Select(x => new Department { Id = x.Id, DepartmentName = x.DepartmentName })
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<Department> FindDepartmentById(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Result> AddDepartment(Department department)
        {
            if (await GetDepartmentByName(department.DepartmentName) != null)
                return new Result { Status = false, Message = ValidationMsg.Exist("Department") };

            _context.Add(department);

            return new Result { Status = true, Data = department };
        }

        public async Task<Result> UpdateDepartment(DepartmentDto departmentDto)
        {
            var existDepartment = await FindDepartmentById(departmentDto.Id);

            if (existDepartment == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            if (existDepartment.DepartmentName.ToLower() != departmentDto.DepartmentName.ToLower())
            {
                if(await GetDepartmentByName(departmentDto.DepartmentName.ToLower()) != null)
                {
                    return new Result { Status = false, Message = ValidationMsg.Exist("This department") };
                }
            }

            var departmentData = _mapper.Map(departmentDto, existDepartment);
            departmentData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

            _context.Attach(departmentData);

            return new Result { Status = true, Data = departmentData };
        }
    }
}
