﻿using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class ForgotPasswordDto
    {
#nullable disable
        [Required(ErrorMessage = "Current password is required")]
        [MinLength(3, ErrorMessage ="Current password must be at least 3 character")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(3, ErrorMessage = "New password must be at least 3 character")]
        public string NewPassword { get; set; }
    }
}
