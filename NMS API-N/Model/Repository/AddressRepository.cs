using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.IRepository;

namespace NMS_API_N.Model.Repository
{
    [Authorize("ManagementRole")]
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

#nullable disable
        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
        }

        public async Task<Address> CheckAddress(int sourceId, string sourceType, int cityId)
        {
            return await _context.Addresses
                .Where(x => x.SourceId == sourceId && x.SourceType == sourceType && x.CityId == cityId)
                .Select(x => new Address
                {
                    SourceId = x.SourceId,
                    SourceType = x.SourceType,
                    AddressDescription = x.AddressDescription

                }).SingleOrDefaultAsync();
        }

        public async Task<Address> GetAddressByIdAsynch(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public void UpdateAddress(Address address)
        {
            _context.Attach(address);
        }
    }
}
