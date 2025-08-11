using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using CompanyRegisterationForm.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CompanyRegisterationForm.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendOtpEmailAsync(string toEmail, string otpCode, string companyName)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("noreply@companyregistration.com"));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = $"OTP Verification - {companyName}";

                var builder = new BodyBuilder();
                builder.HtmlBody = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                        <h2 style='color: #333;'>OTP Verification</h2>
                        <p>Hello,</p>
                        <p>Thank you for registering your company <strong>{companyName}</strong>.</p>
                        <p>Your OTP (One-Time Password) is:</p>
                        <div style='background-color: #f4f4f4; padding: 20px; text-align: center; margin: 20px 0;'>
                            <h1 style='color: #007bff; font-size: 32px; margin: 0;'>{otpCode}</h1>
                        </div>
                        <p>Please enter this OTP in the verification page to complete your registration.</p>
                        <p>This OTP will expire in 10 minutes.</p>
                        <p>If you didn't request this registration, please ignore this email.</p>
                        <br>
                        <p>Best regards,<br>Company Registration Team</p>
                    </div>";

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _configuration["Mailtrap:Host"],
                    int.Parse(_configuration["Mailtrap:Port"]),
                    SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(
                    _configuration["Mailtrap:Username"],
                    _configuration["Mailtrap:Password"]
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception in production
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> ResendOtpEmailAsync(string toEmail, string otpCode, string companyName)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("noreply@companyregistration.com"));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = $"OTP Verification - {companyName} (Resent)";

                var builder = new BodyBuilder();
                builder.HtmlBody = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                        <h2 style='color: #333;'>OTP Verification (Resent)</h2>
                        <p>Hello,</p>
                        <p>You requested a new OTP for your company <strong>{companyName}</strong>.</p>
                        <p>Your new OTP (One-Time Password) is:</p>
                        <div style='background-color: #f4f4f4; padding: 20px; text-align: center; margin: 20px 0;'>
                            <h1 style='color: #007bff; font-size: 32px; margin: 0;'>{otpCode}</h1>
                        </div>
                        <p>Please enter this OTP in the verification page to complete your registration.</p>
                        <p>This OTP will expire in 10 minutes.</p>
                        <p>If you didn't request this OTP, please ignore this email.</p>
                        <br>
                        <p>Best regards,<br>Company Registration Team</p>
                    </div>";

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(
                    _configuration["Mailtrap:Host"],
                    int.Parse(_configuration["Mailtrap:Port"]),
                    SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(
                    _configuration["Mailtrap:Username"],
                    _configuration["Mailtrap:Password"]
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception in production
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
