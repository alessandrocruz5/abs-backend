namespace abs_backend.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PreferredContactMethod { get; set; }
        public string SpecialNotes { get; set; }
    }
}
