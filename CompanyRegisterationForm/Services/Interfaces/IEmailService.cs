namespace CompanyRegisterationForm.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendOtpEmailAsync(string toEmail, string otpCode, string companyName);
        Task<bool> ResendOtpEmailAsync(string toEmail, string otpCode, string companyName);
    }
}
