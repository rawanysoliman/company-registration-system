using Microsoft.AspNetCore.Mvc;
using CompanyRegisterationForm.Models.DTOs;
using CompanyRegisterationForm.Models.ViewModels;
using CompanyRegisterationForm.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CompanyRegisterationForm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IJwtService _jwtService;

        public CompanyController(ICompanyService companyService, IJwtService jwtService)
        {
            _companyService = companyService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> Register([FromForm] CompanyRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<string>.ErrorResult("Validation failed", errors));
            }

            var result = await _companyService.RegisterCompanyAsync(registrationDto);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpPost("validate-otp")]
        public async Task<ActionResult<ApiResponse<string>>> ValidateOtp([FromBody] OtpValidationDto otpDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<string>.ErrorResult("Validation failed", errors));
            }

            var result = await _companyService.ValidateOtpAsync(otpDto);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpPost("set-password")]
        public async Task<ActionResult<ApiResponse<string>>> SetPassword([FromBody] SetPasswordDto passwordDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<string>.ErrorResult("Validation failed", errors));
            }

            var result = await _companyService.SetPasswordAsync(passwordDto);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpPost("resend-otp")]
        public async Task<ActionResult<ApiResponse<string>>> ResendOtp([FromBody] OtpValidationDto otpDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<string>.ErrorResult("Validation failed", errors));
            }

            var result = await _companyService.ResendOtpAsync(otpDto.Email);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<LoginResponse>.ErrorResult("Validation failed", errors));
            }

            var result = await _companyService.LoginAsync(loginDto);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> GetProfile()
        {
            try
            {
                // Get company ID from JWT token
                var companyIdClaim = User.FindFirst("CompanyId");
                if (companyIdClaim == null || !int.TryParse(companyIdClaim.Value, out int companyId))
                {
                    return Unauthorized(ApiResponse<object>.ErrorResult("Invalid token"));
                }

                var result = await _companyService.GetCompanyProfileAsync(companyId);
                
                if (result.Success)
                {
                    return Ok(result);
                }
                
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("An error occurred", new List<string> { ex.Message }));
            }
        }
    }
} 