using System.ComponentModel.DataAnnotations;

namespace CompanyRegisterationForm.Models.Validation
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Password is required");

            var password = value.ToString();

            if (password.Length < 6)
                return new ValidationResult($"Password must be at least 6 characters");

            if (!password.Any(char.IsUpper))
                return new ValidationResult("Password must contain at least one capital letter");

            if (!password.Any(char.IsDigit))
                return new ValidationResult("Password must contain at least one number");

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
                return new ValidationResult("Password must contain at least one special character");

            return ValidationResult.Success;
        }
    }
} 