using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.CustomValidation;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.Helper;
using NMS_API_N.IServices;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;
using NMS_API_N.Pagination;
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

        private IQueryable<EmployeeBasicInfoDto> FetchAllEmployeeBasicInfo()
        {
            return from emp in _context.Employees
                   join com in _context.Companies on emp.CompanyId equals com.Id
                   into sbCom
                   from subcom in sbCom.DefaultIfEmpty()

                   join usr in _context.Users on emp.CreatedBy equals usr.Id

                   select new EmployeeBasicInfoDto
                   {
                       CompanyId = subcom.Id,
                       CompanyName = subcom.CompanyName,
                       Id = emp.Id,
                       FirstName = emp.FirstName,
                       LastName = emp.LastName,
                       Gender = emp.Gender,
                       Dob = emp.Dob,
                       BloodGroup = emp.BloodGroup,
                       MaritalStatus = emp.MaritalStatus,
                       Status = emp.Status,
                       IdType = emp.IdType,
                       IdNo = emp.IdNo,
                       CreatedByName = usr.UserName,
                       CreatedDate = emp.CreatedDate,
                       CreatedBy = emp.CreatedBy,
                       LastUpdatedDate = emp.LastUpdatedDate,
                       DeletedBy = emp.DeletedBy,
                       DeletedDate = emp.DeletedDate,
                       IsActive = emp.IsActive,
                   };
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
            var empDocMasterData = await _context.EmployeeDocumentMaster.FirstOrDefaultAsync(e => e.EmployeeId == employeeDocumentMaster.EmployeeId);

            if (empDocMasterData == null)
            {
                _context.EmployeeDocumentMaster.Add(employeeDocumentMaster);

                await _context.SaveChangesAsync();
                lastInsertedId = _context.EmployeeDocumentMaster.Max(e => e.Id);
            }
            else
            {
                var dataToDto = _mapper.Map<EmployeeDocumentDto>(employeeDocumentMaster);

                var dataToUpdate = _mapper.Map(dataToDto, empDocMasterData);
                dataToUpdate.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());
                dataToUpdate.UpdatedBy = employeeDocumentMaster.CreatedBy;

                _context.Attach(dataToUpdate);

                lastInsertedId = employeeDocumentMaster.Id;
            }

            if (filesCollection.Count > 0)
            {
                var files = await _fileService.CopyFileToServer(filesCollection, "employeedoc", "emp_" + employeeDocumentMaster.EmployeeId);

                foreach (var file in files)
                {
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
                }
            }

            return new Result { Status = true, Message = "Document Data Saved Successfully" };
        }

        public async Task<Result> UpdateEmployeeDocumentMaster(EmployeeDocumentMaseterDto employeeDocument)
        {
            var checkData = await _context.EmployeeDocumentMaster.FirstOrDefaultAsync(x=>x.EmployeeId == employeeDocument.EmployeeId);

            if (checkData == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            var data = _mapper.Map(employeeDocument, checkData);
            data.LastUpdatedDate= DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

            _context.Attach(data);

            return new Result { Status = true, Data = data };

        }

        public async Task<Result> GetEmployeeBasicInfo(int employeeId)
        {
            var data = await FetchAllEmployeeBasicInfo().FirstOrDefaultAsync(e => e.Id == employeeId);

            if (data == null) return new Result { Status = false };

            return new Result { Status = true, Data = data };
        }


        public async Task<PageList<EmployeeBasicInfoDto>> GetAllEmployee(PaginationParams @params)
        {
            var query = FetchAllEmployeeBasicInfo().AsNoTracking().OrderByDescending(e => e.Id);

            return await PageList<EmployeeBasicInfoDto>.CreateAsynch(query, @params.PageNumber, @params.PageSize);
        }


        public async Task<Result> UpdateEmployeeBasicInfo(EmployeeBasicInfoDto employeeBasicInfoDto)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeBasicInfoDto.Id);

            if (checkEmployee == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            var empData = _mapper.Map(employeeBasicInfoDto, checkEmployee);

            empData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

            _context.Attach(empData);

            return new Result { Status = true, Data = employeeBasicInfoDto };
        }

        public async Task<object> GetDocumentInfoByEmployee(int employeeId)
        {
            var docMaster = await _context.EmployeeDocumentMaster.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (docMaster == null) return null;

            var docDetails = await _context.EmployeeDocumentDetails.Where(e => e.EmployeeDocumentMasterId == docMaster.Id).ToListAsync();

            return new
            {
                docmasterData = docMaster,
                docDetailsData = docDetails
            };
        }

        public async Task<bool> DeleteEmployeeDoc(int FileId, int empId)
        {
            var getFileByid = await _context.EmployeeDocumentDetails.FirstOrDefaultAsync(e => e.Id == FileId);

            if (getFileByid != null)
            {
                _fileService.DeleteFile("employeedoc", getFileByid?.FileName, "emp_" + empId);
                _context.EmployeeDocumentDetails.Remove(getFileByid);
                return true;
            }
            return false;

        }
    }
}
