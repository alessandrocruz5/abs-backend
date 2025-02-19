﻿using System.ComponentModel.DataAnnotations;

namespace abs_backend.Models
{
    public class RegisterModel
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string PreferredContactMethod { get; set; }
    }
}
