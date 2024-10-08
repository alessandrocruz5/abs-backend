namespace abs_backend.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Description { get; set; }
        public string? Status { get; set; }
        public User? User{ get; set; }
    }
}
