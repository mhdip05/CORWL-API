using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> GetEmployeeDropdown()
        {
            return await _context.Employees
                .Select(emp => new { employeeId = emp.Id, employeeName = emp.FirstName + " " + emp.LastName })
                .AsNoTracking()
                .ToListAsync();
        }

        public void SaveDocumentInfo(EmployeeDocumentDto employeeDocument)
        {

        }
    }
}
