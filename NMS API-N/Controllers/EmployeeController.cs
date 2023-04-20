using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.IServices;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;
        private readonly IFileServices _fileService;
        private IWebHostEnvironment _environment;

        public EmployeeController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
            _fileService = (IFileServices)ApplicationServiceExtension.serviceProvider.GetRequiredService(typeof(IFileServices));
            _environment = (IWebHostEnvironment)ApplicationServiceExtension.serviceProvider.GetRequiredService(typeof(IWebHostEnvironment));
        }

        [HttpGet("GetEmployeeDropdown")]

        public async Task<IActionResult> GetEmployeeDropdown()
        {
            return Ok(await _uot.EmployeeRepository.GetEmployeeDropdown());
        }

        [HttpPost("SaveDocumentInfo")]
        public async Task<ActionResult> SaveDocumentInfo([FromForm] EmployeeDocumentDto employeeDocument)
        {
            List<EmployeeDocument> empDoc = new List<EmployeeDocument>();


            var files = await _fileService.CopyFileToServer(employeeDocument.Files, "mhdip");

            foreach (var file in files)
            {
                // save to data base
                empDoc.Add(new EmployeeDocument
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FilePath = file.FilePath,
                    FileSize= file.FileSize,
                    fileExtension = file.fileExtension
                });
            }

            return Ok(new { employeeDocument, empDoc});
        }
    }
}
