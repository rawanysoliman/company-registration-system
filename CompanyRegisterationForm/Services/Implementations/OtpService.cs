using CompanyRegisterationForm.Services.Interfaces;

namespace CompanyRegisterationForm.Services.Implementations
{
    public class OtpService : IOtpService
    {
        private readonly Dictionary<string, (string Otp, DateTime Expiry)> _otpStorage = new();
        private readonly object _lock = new object();

        public string GenerateOtp()
        {
            // Generate a 6-digit OTP
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void StoreOtp(string email, string otp)
        {
            lock (_lock)
            {
                // Store OTP with 10 minutes expiry
                _otpStorage[email] = (otp, DateTime.UtcNow.AddMinutes(10));
            }
        }

        public bool ValidateOtp(string email, string otp)
        {
            lock (_lock)
            {
                if (_otpStorage.TryGetValue(email, out var storedOtp))
                {
                    // Check if OTP is expired
                    if (DateTime.UtcNow > storedOtp.Expiry)
                    {
                        _otpStorage.Remove(email);
                        return false;
                    }

                    // Check if OTP matches
                    if (storedOtp.Otp == otp)
                    {
                        _otpStorage.Remove(email); // Remove after successful validation
                        return true;
                    }
                }
                return false;
            }
        }

        public void RemoveOtp(string email)
        {
            lock (_lock)
            {
                _otpStorage.Remove(email);
            }
        }

        
    }
}
