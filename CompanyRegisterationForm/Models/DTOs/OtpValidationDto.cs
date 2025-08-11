using System.ComponentModel.DataAnnotations;

namespace CompanyRegisterationForm.Models.DTOs
{
    public class OtpValidationDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "OTP code is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be exactly 6 characters")]
        public string OtpCode { get; set; } = string.Empty;
    }
} 