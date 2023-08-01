using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CORWL_API.Extension;
using CORWL_API.Helper;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;

namespace CORWL_API.Controllers.v1
{
    [Authorize("ManagementRole")]
    public class CityController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpGet("GetAllCity")]
        public async Task<IActionResult> GetAllCity()
        {
            return Ok(await _uot.CityRepository.GetAllCity());
        }

        [HttpGet("GetCityByCountry")]
        public async Task<IActionResult> GetCityByCountry(int countryId)
        {
            return Ok(await _uot.CityRepository.GetCityByCountry(countryId));
        }

        [HttpPost("add-city")]
        public async Task<ActionResult<CityDto>> AddCity(CityDto cityDto)
        {
            var cityData = _mapper.Map<City>(cityDto);

            var checkCity = await _uot.CityRepository.GetCityByName(cityData.CityName.ToLower(), cityData.CountryId);

            if (checkCity != null) return BadRequest("City Already Exist");

            cityData.CityName = cityData.CityName.ToLower();
            cityData.CreatedBy = int.Parse(User.GetUserId());
            cityData.CreatedDate = DateTime.Now;

            _uot.CityRepository.AddCity(cityData);

            if (await _uot.Complete())
                return Ok(cityData);

            return BadRequest("Failed To Add City");
        }

        [HttpPut("update-city")]
        public async Task<ActionResult> UpdateCity(CityDto cityDto)
        {
            var checkCity = await _uot.CityRepository.GetCityById(cityDto.Id);

            if (checkCity == null) return BadRequest("No Data Found");

            if (checkCity.CityName == cityDto.CityName.ToLower() && checkCity.CountryId == cityDto.CountryId)
                return BadRequest("Updating with same name is not allowed");

            var cityData = _mapper.Map(cityDto, checkCity);

            cityData.UpdatedBy = int.Parse(User.GetUserId());
            cityData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());
            cityData.UpdatedCount += 1;

            _uot.CityRepository.UpdateCity(cityData);

            if (await _uot.Complete()) return NoContent();

            return BadRequest("Failed to updated city");
        }
    }
}
