using System.ComponentModel.DataAnnotations;
using CompanyRegisterationForm.Models.Validation;

namespace CompanyRegisterationForm.Models.DTOs
{
    public class SetPasswordDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;



        [Required(ErrorMessage = "New Password is required")]
        [PasswordComplexity]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
} 