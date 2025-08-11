using CompanyRegisterationForm.Data.Entities;

namespace CompanyRegisterationForm.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<Company> CreateAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Company>> GetAllAsync();
    }
} 