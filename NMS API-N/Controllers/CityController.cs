using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.Extension;
using NMS_API_N.Helper;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
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

            if ((checkCity.CityName == cityDto.CityName.ToLower()) && (checkCity.CountryId == cityDto.CountryId))
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
