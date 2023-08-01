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
    public class CurrencyController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public CurrencyController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        private async Task<bool> GetCurrency(string currencyName)
        {
            var checkCurrency = await _uot.CurrencyRepository.GetCurrencyByName(currencyName);

            if (checkCurrency != null) return true;

            return false;
        }

        [HttpGet("GetAllCurrencies")]
        public async Task<ActionResult<Currency>> GetAllCurrencies()
        {
            return Ok(await _uot.CurrencyRepository.GetAllCurrency());
        }

        [HttpGet("GetCurrencyDropdown")]
        public async Task<ActionResult> GetCurrencyDropdown()
        {
            return Ok(await _uot.CurrencyRepository.GetCurrencyDropdown());
        }

        [HttpGet("GetCurrencyById/{id}")]
        public async Task<ActionResult<Currency>> GetCurrencyById(int id)
        {
            return Ok(await _uot.CurrencyRepository.GetCurrencyById(id));
        }

        [HttpPost("SetCurrency")]
        public async Task<ActionResult<CurrencyDto>> SetCurrency(CurrencyDto currencyDto)
        {
            var currencyData = _mapper.Map<Currency>(currencyDto);

            if (await GetCurrency(currencyDto.CurrencyName.ToUpper()))
                return BadRequest("Currency Already Exist");

            currencyData.CurrencyName = currencyData.CurrencyName.ToUpper();
            currencyData.CreatedBy = int.Parse(User.GetUserId());

            _uot.CurrencyRepository.AddCurrency(currencyData);

            if (await _uot.Complete())
                return Ok(currencyData);

            return BadRequest("Failed to Add Currency");
        }

        [HttpPut("UpdateCurrency")]
        public async Task<ActionResult<CurrencyDto>> UpdateCurrency(CurrencyDto currencyDto)
        {
            var checkCurrency = await _uot.CurrencyRepository.GetCurrencyById(currencyDto.Id);

            if (checkCurrency == null) return BadRequest("No Data Found");

            if (checkCurrency.CurrencyName != currencyDto.CurrencyName.ToUpper())
            {
                if (await GetCurrency(currencyDto.CurrencyName.ToUpper()))
                    return BadRequest("Currency Already Exist");
            }

            var currencyData = _mapper.Map(currencyDto, checkCurrency);

            currencyData.CurrencyName = currencyData.CurrencyName.ToUpper();
            currencyData.UpdatedBy = int.Parse(User.GetUserId());
            currencyData.LastUpdatedDate = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());

            _uot.CurrencyRepository.UpdateCurrency(currencyData);

            if (await _uot.Complete())
                return Ok(currencyData);

            return BadRequest("Failed to update Currency");
        }

        [HttpDelete("DeleteCurrency")]
        public async Task<ActionResult> DeleteCurrency(int id)
        {
            var data = await _uot.CurrencyRepository.DeleteCurrency(id);

            if (data == 0) return BadRequest("No Data Found");

            if (data == 1)
            {
                if (await _uot.Complete()) return Ok(new SuccessMessageDto { Message = "Currency Deleted Successfully" });
            };

            return BadRequest("Something went wrong");
        }

    }
}
