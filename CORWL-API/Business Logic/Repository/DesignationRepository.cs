using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CORWL_API.CustomValidation;
using CORWL_API.DbContext;
using CORWL_API.Helper;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Business_Logic.IRepository;

namespace CORWL_API.Business_Logic.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DesignationRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IQueryable<DesignationDto> FetchAllDesignation()
        {
            return from ds in _context.Designations
                   join dp in _context.Departments on ds.DepartmentId equals dp.Id
                   join usr in _context.Users on ds.CreatedBy equals usr.Id

                   select new DesignationDto
                   {
                       Id = ds.Id,
                       DesignationName = ds.DesignationName,
                       Abbreviation = ds.Abbreviation,
                       DepartmentId = ds.DepartmentId,
                       DepartmentName = dp.DepartmentName,
                       CreatedBy = usr.Id,
                       CreatedByName = usr.UserName,
                       CreatedDate = ds.CreatedDate,
                       LastUpdatedDate = ds.LastUpdatedDate,
                   };
        }

        public async Task<IEnumerable<DesignationDto>> GetAllDesignation()
        {
            return await FetchAllDesignation().AsNoTracking().ToListAsync();
        }

        public async Task<DesignationDto> GetDesignationById(int id)
        {
            return await FetchAllDesignation().Where(e => e.Id == id).AsNoTracking().FirstAsync();
        }

        public async Task<IEnumerable<object>> GetDesignationDropdown()
        {
            return await _context.Designations
               .Select(x => new { DesignationId = x.Id, x.DesignationName })
               .AsNoTracking()
               .ToListAsync();
        }

#nullable disable
        public async Task<Designation> GetDesignationByName(string designationName)
        {
            return await _context.Designations
                .Where(x => x.DesignationName == designationName)
                .Select(x => new Designation { Id = x.Id, DesignationName = x.DesignationName })
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<Designation> FindDesignationById(int id)
        {
            return await _context.Designations.FindAsync(id);
        }

        public async Task<Result> AddDesignation(Designation designation)
        {
            if (await GetDesignationByName(designation.DesignationName) != null)
                return new Result { Status = false, Message = ValidationMsg.Exist("Designation") };

            _context.Add(designation);

            return new Result { Status = true, Data = designation };
        }

        public async Task<Result> UpdateDesignation(DesignationDto designationDto)
        {
            var existDesignation = await FindDesignationById(designationDto.Id);

            if (existDesignation == null) return new Result { Status = false, Message = ValidationMsg.NoRecordFound() };

            if (existDesignation.DesignationName.ToLower() != designationDto.DesignationName.ToLower())
            {
                if (await GetDesignationByName(designationDto.DesignationName.ToLower()) != null)
                {
                    return new Result { Status = false, Message = ValidationMsg.Exist("This designation") };
                }
            }

            var designationData = _mapper.Map(designationDto, existDesignation);
            designationData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

            _context.Attach(designationData);

            return new Result { Status = true, Data = designationData };
        }
    }
}
