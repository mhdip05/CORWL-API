using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.CustomValidation;
using NMS_API_N.Extension;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Unit_Of_Work;

namespace NMS_API_N.Controllers
{
    [Authorize("ManagementRole")]
    public class CountryController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpGet("GetAllCountries")]
        public async Task<IActionResult> GetAllCountries()
        {
            return Ok(await _uot.CountryRepository.GetAllCountry());
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _uot.CountryRepository.GetCountries());
        }

        [HttpPost("add-country")]
        public async Task<ActionResult<CountryDto>> AddCountry(CountryDto country)
        {
            var countryData = _mapper.Map<Country>(country);

            var checkCountry = await _uot.CountryRepository.GetCountryByName(countryData.CountryName.ToLower());

            if (checkCountry != null) return BadRequest("Country Already Exist");

            countryData.CountryName = countryData.CountryName.ToLower();
            countryData.CreatedBy = int.Parse(User.GetUserId());

            _uot.CountryRepository.AddCountry(countryData);

            if (await _uot.Complete())
                return Ok(countryData);

            return BadRequest("Failed To Add Country");
        }

        [HttpPut("update-country")]
        public async Task<ActionResult> UpdateCountry(CountryDto countryDto)
        {
            countryDto.UpdatedBy = int.Parse(User.GetUserId());

            var res = await _uot.CountryRepository.UpdateCountry(countryDto);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete()) return Ok(new SuccessMessageDto { Message = "Country updated successfully"});

            return BadRequest(ValidationMsg.SomethingWrong("updating country"));
        }
    }
}
