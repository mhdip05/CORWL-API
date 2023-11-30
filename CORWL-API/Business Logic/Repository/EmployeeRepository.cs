using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CORWL_API.CustomValidation;
using CORWL_API.DbContext;
using CORWL_API.Extension;
using CORWL_API.Helper;
using CORWL_API.IServices;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Business_Logic.IRepository;
using CORWL_API.Pagination;
using CORWL_API.Unit_Of_Work;
using Microsoft.AspNetCore.Mvc;

namespace CORWL_API.Business_Logic.Repository
{

    public class EmployeeRepository : IEmployeeRepository
    {
#nullable disable
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IFileServices _fileService;
       // private readonly UserManager<User> _userManager;
       // private readonly IAzureBlob _azureBlob;

        public EmployeeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _fileService = (IFileServices)ApplicationServiceExtension.serviceProvider.GetRequiredService(typeof(IFileServices));
            //_userManager = (UserManager<User>)IdentityServiceExtension.serviceProvider.GetRequiredService(typeof(UserManager<User>));
           // _azureBlob = (IAzureBlob)IdentityServiceExtension.serviceProvider.GetRequiredService(typeof(IAzureBlob));
        }

        private IQueryable<EmployeeBasicInfoDto> FetchAllEmployeeBasicInfo()
        {
            return from emp in _context.Employees

                   join com in _context.Companies on emp.CompanyId equals com.Id
                   into sbCom
                   from subcom in sbCom.DefaultIfEmpty()

                   join empUsr in _context.Users on emp.Id equals empUsr.EmployeeId
                   into sbEmpUsr
                   from subEmpUsr in sbEmpUsr.DefaultIfEmpty()

                   join usr in _context.Users on emp.CreatedBy equals usr.Id

                   select new EmployeeBasicInfoDto
                   {
                       CompanyId = subcom.Id,
                       CompanyName = subcom.CompanyName,
                       Id = emp.Id,
                       FirstName = emp.FirstName,
                       LastName = emp.LastName,
                       UserName = subEmpUsr.UserName,
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


        public async Task<EmployeeJobDetailsDto> GetEmployeeJobDetails(int employeeId)
        {
            return await (from empj in _context.EmployeeJobDetails.Where(e => e.EmployeeId == employeeId)

                          join com in _context.Companies on empj.CompanyId equals com.Id
                          join br in _context.Branches on empj.BranchId equals br.Id
                          join dept in _context.Departments on empj.DepartmentId equals dept.Id
                          join des in _context.Designations on empj.DesignationId equals des.Id

                          join emp in _context.Employees on empj.SupervisorId equals emp.Id
                          into sbEmp
                          from subEmp in sbEmp.DefaultIfEmpty()

                          select new EmployeeJobDetailsDto
                          {
                              Id = empj.Id,
                              EmployeeId = empj.EmployeeId,
                              CompanyId = com.Id,
                              CompanyName = com.CompanyName,
                              BranchId = br.Id,
                              BranchName = br.BranchName,
                              DepartmentId = dept.Id,
                              DepartmentName = dept.DepartmentName,
                              DesignationId = des.Id,
                              DesignationName = des.DesignationName,
                              SupervisorId = subEmp.Id,
                              SupervisorName = subEmp.FirstName + " " + subEmp.LastName,
                              StaffGrade = empj.StaffGrade,
                              ConfirmationDate = empj.ConfirmationDate,
                              JoiningDate = empj.JoiningDate,
                              ReportingMethod = empj.ReportingMethod,
                          }).FirstOrDefaultAsync();
        }

        public async Task<Result> SaveEmployeeJobDetails(EmployeeJobDetails employeeJobDetails)
        {
            var checkEmployee = await _context.EmployeeJobDetails.FirstOrDefaultAsync(e => e.EmployeeId == employeeJobDetails.EmployeeId);

            if (checkEmployee != null)
                return new Result { Status = false, Message = ValidationMsg.Exist("employee job details") };

            _context.EmployeeJobDetails.Add(employeeJobDetails);

            return new Result { Status = true };

        }

        public async Task<Result> UpdateEmployeeJobDetails(EmployeeJobDetailsDto employeeJobDetails)
        {
            var jobDetails = await _context.EmployeeJobDetails.FindAsync(employeeJobDetails.Id);

            if (jobDetails == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            var mapData = _mapper.Map(employeeJobDetails, jobDetails);
            mapData.LastUpdatedDate = DateTime.Now;

            _context.Attach(mapData);

            return new Result { Status = true, Data = mapData };



        }

        public async Task<Result> SaveDocument(EmployeeDocumentMaster employeeDocumentMaster, List<IFormFile> filesCollection, IAzureBlob azureBlob)
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
                var files = await azureBlob.UploadFileToAzureStorage(filesCollection, "employeedoc", "emp_" + employeeDocumentMaster.EmployeeId);

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
            var checkData = await _context.EmployeeDocumentMaster.FirstOrDefaultAsync(x => x.EmployeeId == employeeDocument.EmployeeId);

            if (checkData == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            var data = _mapper.Map(employeeDocument, checkData);
            data.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

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

        public async Task<bool> DeleteEmployeeDocsFromAzure(int FileId, IAzureBlob azureBlob)
        {
            var getFileByid = await _context.EmployeeDocumentDetails.FirstOrDefaultAsync(e => e.Id == FileId);

            if (getFileByid != null)
            {
                var blob = await azureBlob.DeleteFileFromAzureStorage(getFileByid.FilePath, getFileByid.FileName);
                if (blob)
                {
                    _context.EmployeeDocumentDetails.Remove(getFileByid);
                    return true;
                }
            }
            return false;
        }

    }
}
