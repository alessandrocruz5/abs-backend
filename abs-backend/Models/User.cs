using System.ComponentModel.DataAnnotations.Schema;

namespace abs_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string PasswordHash { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? EmailVerificationToken { get; set; }

        // Customer-specific fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PreferredContactMethod { get; set; }

        // Navigation property
        public ICollection<Appointment> Appointments { get; set; }
    }
}
