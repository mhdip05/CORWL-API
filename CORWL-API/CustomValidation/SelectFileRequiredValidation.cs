using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CORWL_API.CustomValidation
{
    public  class SelectFileRequiredValidation : RequiredAttribute
    {
#nullable disable
        public bool IsRequired { get; set; }

        public SelectFileRequiredValidation(bool isRequired)
        {
            IsRequired = isRequired;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsRequired == false)
            {
                return base.IsValid(value, validationContext);
            }

            // If the condition is not met, always return success
            return ValidationResult.Success;
        }
    }
}
