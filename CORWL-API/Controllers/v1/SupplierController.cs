using AutoMapper;
using CORWL_API.CustomValidation;
using CORWL_API.Extension;
using CORWL_API.Model.DTO;
using CORWL_API.Model.Entities;
using CORWL_API.Unit_Of_Work;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace CORWL_API.Controllers.v1
{
    public class SupplierController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly IMapper _mapper;

        public SupplierController(IUnitOfWork uot, IMapper mapper)
        {
            _uot = uot;
            _mapper = mapper;
        }

        [HttpPost("SaveSupplier")]
        public async Task<IActionResult> SaveSupplier(SupplierDto supplierDto)
        {
            var supplierData = _mapper.Map<Supplier>(supplierDto);
            supplierData.CreatedBy = int.Parse(User.GetUserId());

            var res = await _uot.SupplierRepository.SaveSupplierInfo(supplierData);

            if (res.Status == false) return BadRequest(res.Message);

            if (await _uot.Complete())
                return Ok(new Result { Message = res.Message, Data = supplierData });

            return BadRequest(ValidationMsg.SomethingWrong());
        }



    }
}
