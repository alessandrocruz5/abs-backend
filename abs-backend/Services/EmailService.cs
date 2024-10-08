using System.Net;
using System.Net.Mail;

namespace abs_backend.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public async Task SendEmailAsync(string to, string subject, string body)
        //{
        //    var smtpServer = _configuration["EmailSettings:SmtpServer"];
        //    var port = int.Parse(_configuration["EmailSettings:Port"]);
        //    var username = _configuration["EmailSettings:Username"];
        //    var password = _configuration["EmailSettings:Password"];

        //    using var client = new SmtpClient(smtpServer, port)
        //    {
        //        Credentials = new NetworkCredential(username, password),
        //        EnableSsl = true
        //    };

        //    await client.SendMailAsync(new MailMessage(from: username, to: to, subject: subject, body: body)
        //    {
        //        IsBodyHtml = true
        //    });
        //}

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            if (string.IsNullOrEmpty(to))
                throw new ArgumentException("Recipient email address cannot be null or empty", nameof(to));
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentException("Email subject cannot be null or empty", nameof(subject));
            if (string.IsNullOrEmpty(body))
                throw new ArgumentException("Email body cannot be null or empty", nameof(body));

            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var portString = _configuration["EmailSettings:Port"];
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];

            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new InvalidOperationException("Email configuration is incomplete. Please check your settings.");

            // Handle port parsing with a default value
            if (!int.TryParse(portString, out int port))
            {
                // Log a warning that we're using a default port
                Console.WriteLine($"Warning: Invalid or missing port configuration. Using default port 587.");
                port = 587; // Common default for SMTP with TLS
            }

            using var client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            try
            {
                await client.SendMailAsync(new MailMessage(from: username, to: to, subject: subject, body: body)
                {
                    IsBodyHtml = true
                });
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw new InvalidOperationException($"Failed to send email. Please check the email configuration and try again.", ex);
            }
        }

        public async Task SendVerificationEmailAsync(string to, string verificationToken)
        {
            var subject = "Verify your Email Address";
            var verificationLink = $"{_configuration["ApplicationUrl"]}/verify-email?token={verificationToken}";
            var body = $"Please click the link below to verify your email address:<br><a href='{verificationLink}'>Verify Email</a>";

            await SendEmailAsync(to, subject, body);
        }

        public async Task SendBookingConfirmationAsync(string to, string bookingDetails)
        {
            var subject = "Booking Confirmation";
            var body = $"Your booking has been confirmed. Details:<br>{bookingDetails}";

            await SendEmailAsync(to, subject, body);
        }
    }
}
