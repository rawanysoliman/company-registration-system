using System.ComponentModel.DataAnnotations;

namespace CompanyRegisterationForm.Data.Entities
{
    public class Company
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string ArabicName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        public string EnglishName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        
        [MaxLength(500)]
        public string? WebsiteUrl { get; set; }
        
        [MaxLength(500)]
        public string? LogoPath { get; set; }
        
        public string? PasswordHash { get; set; }
        
        public bool IsEmailVerified { get; set; } = false;
        
        public string? OtpCode { get; set; }
        
        public DateTime? OtpExpiry { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
} 