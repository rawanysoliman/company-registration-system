using CompanyRegisterationForm.Services.Interfaces;

namespace CompanyRegisterationForm.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public FileService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<string> UploadLogoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file provided");

            // Validations
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");

            // file size 
            if (file.Length > 5 * 1024 * 1024)
                throw new ArgumentException("File size too large. Maximum size is 5MB.");

            var webRootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var uploadsPath = Path.Combine(webRootPath, "uploads", "logos");
            
          
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }



        public string GetLogoUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            var baseUrl = _configuration["BaseUrl"];
            return $"{baseUrl}/uploads/logos/{fileName}";
        }
    }
} 