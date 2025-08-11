using CompanyRegisterationForm.Models.DTOs;
using CompanyRegisterationForm.Models.ViewModels;

namespace CompanyRegisterationForm.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ApiResponse<string>> RegisterCompanyAsync(CompanyRegistrationDto registrationDto);
        Task<ApiResponse<string>> ValidateOtpAsync(OtpValidationDto otpDto);
        Task<ApiResponse<string>> SetPasswordAsync(SetPasswordDto passwordDto);
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<object>> GetCompanyProfileAsync(int companyId);
        Task<ApiResponse<string>> ResendOtpAsync(string email);
    }
} 