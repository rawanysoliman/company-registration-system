using CompanyRegisterationForm.Data.Entities;

namespace CompanyRegisterationForm.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Company company);
        bool ValidateToken(string token);
        int GetCompanyIdFromToken(string token);
    }
} 