using BCrypt.Net;
using CompanyRegisterationForm.Data.Entities;
using CompanyRegisterationForm.Models.DTOs;
using CompanyRegisterationForm.Models.ViewModels;
using CompanyRegisterationForm.Repository.Interfaces;
using CompanyRegisterationForm.Services.Interfaces;

namespace CompanyRegisterationForm.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJwtService _jwtService;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IOtpService _otpService;

        public CompanyService(
            ICompanyRepository companyRepository,
            IJwtService jwtService,
            IFileService fileService,
            IEmailService emailService,
            IOtpService otpService)
        {
            _companyRepository = companyRepository;
            _jwtService = jwtService;
            _fileService = fileService;
            _emailService = emailService;
            _otpService = otpService;
        }

        public async Task<ApiResponse<string>> RegisterCompanyAsync(CompanyRegistrationDto registrationDto)
        {
            try
            {
                if (await _companyRepository.EmailExistsAsync(registrationDto.Email))
                {
                    return ApiResponse<string>.ErrorResult("Email address is already registered");
                }

                string? logoPath = null;
                if (registrationDto.Logo != null)
                {
                    try
                    {
                        logoPath = await _fileService.UploadLogoAsync(registrationDto.Logo);
                    }
                    catch (ArgumentException ex)
                    {
                        return ApiResponse<string>.ErrorResult(ex.Message);
                    }
                }


                var otp = _otpService.GenerateOtp();
                var otpExpiry = DateTime.UtcNow.AddMinutes(10);

                var company = new Company
                {
                    ArabicName = registrationDto.ArabicName,
                    EnglishName = registrationDto.EnglishName,
                    Email = registrationDto.Email,
                    PhoneNumber = registrationDto.PhoneNumber,
                    WebsiteUrl = registrationDto.WebsiteUrl,
                    LogoPath = logoPath,
                    OtpCode = otp,
                    OtpExpiry = otpExpiry,
                    IsEmailVerified = false
                };

                await _companyRepository.CreateAsync(company);

                _otpService.StoreOtp(registrationDto.Email, otp);

                var emailSent = await _emailService.SendOtpEmailAsync(registrationDto.Email, otp, registrationDto.EnglishName);
                
                if (!emailSent)
                {
                    Console.WriteLine($"Failed to send OTP email to {registrationDto.Email}");
                }

                return ApiResponse<string>.SuccessResult("Company registered successfully. Please check your email for OTP verification.");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.ErrorResult("An error occurred during registration", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<string>> ValidateOtpAsync(OtpValidationDto otpDto)
        {
            try
            {
                var company = await _companyRepository.GetByEmailAsync(otpDto.Email);
                if (company == null)
                {
                    return ApiResponse<string>.ErrorResult("Company not found");
                }

                if (company.IsEmailVerified)
                {
                    return ApiResponse<string>.ErrorResult("Email is already verified");
                }


                if (!_otpService.ValidateOtp(otpDto.Email, otpDto.OtpCode))
                {
                    return ApiResponse<string>.ErrorResult("Invalid or expired OTP code");
                }

                
                company.IsEmailVerified = true;
                
                await _companyRepository.UpdateAsync(company);

                return ApiResponse<string>.SuccessResult("OTP validated successfully. Please set your password.");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.ErrorResult("An error occurred during OTP validation", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<string>> SetPasswordAsync(SetPasswordDto passwordDto)
        {
            try
            {
                var company = await _companyRepository.GetByEmailAsync(passwordDto.Email);
                if (company == null)
                {
                    return ApiResponse<string>.ErrorResult("Company not found");
                }

                if (!company.IsEmailVerified)
                {
                    return ApiResponse<string>.ErrorResult("Email is not verified. Please verify your email first.");
                }

    
                if (string.IsNullOrEmpty(company.OtpCode))
                {
                    return ApiResponse<string>.ErrorResult("No OTP found. Please complete OTP validation first.");
                }


                var passwordHash = BCrypt.Net.BCrypt.HashPassword(passwordDto.NewPassword);

                company.PasswordHash = passwordHash;
                company.OtpCode = null;
                company.OtpExpiry = null;
                await _companyRepository.UpdateAsync(company);

  
                _otpService.RemoveOtp(passwordDto.Email);




                return ApiResponse<string>.SuccessResult("Password set successfully. You can now login.");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.ErrorResult("An error occurred while setting password", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<string>> ResendOtpAsync(string email)
        {
            try
            {
                var company = await _companyRepository.GetByEmailAsync(email);
                if (company == null)
                {
                    return ApiResponse<string>.ErrorResult("Company not found");
                }

                if (company.IsEmailVerified)
                {
                    return ApiResponse<string>.ErrorResult("Email is already verified");
                }

              
                var newOtp = _otpService.GenerateOtp();
                var otpExpiry = DateTime.UtcNow.AddMinutes(10);

             
                company.OtpCode = newOtp;
                company.OtpExpiry = otpExpiry;
                await _companyRepository.UpdateAsync(company);

               
                _otpService.StoreOtp(email, newOtp);

               
                var emailSent = await _emailService.ResendOtpEmailAsync(email, newOtp, company.EnglishName);
                
                if (!emailSent)
                {
                    return ApiResponse<string>.ErrorResult("Failed to send OTP email. Please try again.");
                }

                return ApiResponse<string>.SuccessResult("New OTP sent successfully. Please check your email.");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.ErrorResult("An error occurred while resending OTP", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var company = await _companyRepository.GetByEmailAsync(loginDto.Email);
                if (company == null)
                {
                    return ApiResponse<LoginResponse>.ErrorResult("Invalid email or password");
                }

                if (!company.IsEmailVerified)
                {
                    return ApiResponse<LoginResponse>.ErrorResult("Email is not verified. Please verify your email first.");
                }

                if (string.IsNullOrEmpty(company.PasswordHash))
                {
                    return ApiResponse<LoginResponse>.ErrorResult("Password not set. Please complete the registration process.");
                }

                
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, company.PasswordHash))
                {
                    return ApiResponse<LoginResponse>.ErrorResult("Invalid email or password");
                }

                // Generate JWT token
                var token = _jwtService.GenerateToken(company);

                var loginResponse = new LoginResponse
                {
                    Token = token,
                    CompanyName = company.EnglishName,
                    CompanyLogo = _fileService.GetLogoUrl(company.LogoPath ?? ""),
                    ExpiresAt = DateTime.UtcNow.AddHours(24)
                };

                return ApiResponse<LoginResponse>.SuccessResult(loginResponse, "Login successful");
            }
            catch (Exception ex)
            {
                return ApiResponse<LoginResponse>.ErrorResult("An error occurred during login", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<object>> GetCompanyProfileAsync(int companyId)
        {
            try
            {
                var company = await _companyRepository.GetByIdAsync(companyId);
                if (company == null)
                {
                    return ApiResponse<object>.ErrorResult("Company not found");
                }

                var profile = new
                {
                    company.Id,
                    company.ArabicName,
                    company.EnglishName,
                    company.Email,
                    company.PhoneNumber,
                    company.WebsiteUrl,
                    LogoUrl = _fileService.GetLogoUrl(company.LogoPath ?? ""),
                    company.CreatedAt
                };

                return ApiResponse<object>.SuccessResult(profile, "Company profile retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResult("An error occurred while retrieving company profile", new List<string> { ex.Message });
            }
        }


    }
} 