﻿using NMS_API_N.CustomValidation;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Model.IRepository
{
    public interface IDepartmentRepository
    {
        Task<Result> AddDepartment(Department department);
        Task<Department> GetDepartmentByName(string departmentName);
        Task<DepartmentDto> GetDepartmentById(int id);
        Task<Result> UpdateDepartment(DepartmentDto departmentDto);
        Task<IEnumerable<DepartmentDto>> GetAllDepartment();
        Task<IEnumerable<object>> GetDepartmentDropdown();
    }
}
