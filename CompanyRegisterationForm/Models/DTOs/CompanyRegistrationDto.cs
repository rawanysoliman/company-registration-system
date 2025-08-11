using System.ComponentModel.DataAnnotations;

namespace CompanyRegisterationForm.Models.DTOs
{
    public class CompanyRegistrationDto
    {
        [Required(ErrorMessage = "Company Arabic Name is required")]
        [MaxLength(200, ErrorMessage = "Company Arabic Name cannot exceed 200 characters")]
        public string ArabicName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company English Name is required")]
        [MaxLength(200, ErrorMessage = "Company English Name cannot exceed 200 characters")]
        public string EnglishName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? PhoneNumber { get; set; }

        [Url(ErrorMessage = "Invalid website URL format")]
        [MaxLength(500, ErrorMessage = "Website URL cannot exceed 500 characters")]
        public string? WebsiteUrl { get; set; }

        public IFormFile? Logo { get; set; }
    }
} 