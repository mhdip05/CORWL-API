using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class AddressDto
    {
        public int AddressId { get; set; }

#nullable disable
        [Required(ErrorMessage = "Address Description is Required")]
        [MinLength(3, ErrorMessage = "Minium length of address description must be 3")]
        public string AddressDescription { get; set; }

        [Required(ErrorMessage = "Please select the source")]
        [Range(1, int.MaxValue, ErrorMessage = "Make sure you have selected valid source")]
        public int SourceId { get; set; }

        [Required(ErrorMessage = "Please Enter Source Type")]
        [MinLength(2, ErrorMessage = "Minimum length of source type is 2")]
        public string SourceType { get; set; }

        [Required(ErrorMessage = "Pleaser Enter Phone No")]
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string AddressType { get; set; }

        [Required(ErrorMessage = "Please Select City")]
        [Range(1, int.MaxValue, ErrorMessage = "make sure you have selected a valid city")]
        public int CityId { get; set; }
    }
}
