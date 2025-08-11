namespace CompanyRegisterationForm.Services.Interfaces
{
    public interface IOtpService
    {
        string GenerateOtp();
        bool ValidateOtp(string email, string otp);
        void StoreOtp(string email, string otp);
        void RemoveOtp(string email);
    }
}
