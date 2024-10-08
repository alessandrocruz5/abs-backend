namespace abs_backend.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendVerificationEmailAsync(string to, string verificationToken);
        Task SendBookingConfirmationAsync(string to, string bookingDetails);
    }
}
