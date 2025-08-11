namespace CompanyRegisterationForm.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadLogoAsync(IFormFile file);
        string GetLogoUrl(string fileName);
    }
} 