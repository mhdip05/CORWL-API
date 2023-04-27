using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.IServices;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Model.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
#nullable disable
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IFileServices _fileService;
        public EmployeeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _fileService = (IFileServices)ApplicationServiceExtension.serviceProvider.GetRequiredService(typeof(IFileServices));
        }

        public async Task<IEnumerable<object>> GetEmployeeDropdown()
        {
            return await _context.Employees
                .Select(emp => new { employeeId = emp.Id, employeeName = emp.FirstName + " " + emp.LastName })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Result> SaveEmployeeBasicInfo(Employee employee)
        {
            var checkEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.IdType == employee.IdType && e.IdNo == employee.IdNo);

            if (checkEmployee != null)
                return new Result { Status = false, Message = ValidationMsg.Exist("employee") };

            _context.Employees.Add(employee);

            return new Result { Status = true, Data = employee };
        }


        public async Task<Result> SaveDocument(EmployeeDocumentMaster employeeDocumentMaster, List<IFormFile> filesCollection)
        {
            int lastInsertedId = 0;
            var check = await _context.EmployeeDocumentMaster.FirstOrDefaultAsync(e => e.DocumentName == employeeDocumentMaster.DocumentName && e.EmployeeId == employeeDocumentMaster.EmployeeId);

            if (check == null)
            {
                _context.EmployeeDocumentMaster.Add(employeeDocumentMaster);
                await _context.SaveChangesAsync();

                lastInsertedId = _context.EmployeeDocumentMaster.Max(e => e.Id);
            }


            var files = await _fileService.CopyFileToServer(filesCollection, "employeedoc", "emp_" + employeeDocumentMaster.EmployeeId);

            foreach (var file in files)
            {
                // save to data base
                var empDoc = new EmployeeDocumentDetails
                {
                    EmployeeDocumentMasterId = lastInsertedId,
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FilePath = file.FilePath,
                    FileSize = file.FileSize,
                    fileExtension = file.fileExtension,
                    FileType = file.FileType,
                    FileUnit = file.FileUnit,
                };

                _context.EmployeeDocumentDetails.Add(empDoc);
                // await _context.SaveChangesAsync();
            }

            return new Result { Status = true };
        }


    }
}
