namespace CompanyRegisterationForm.Models.ViewModels
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyLogo { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
} 