using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interactive_Agency.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Please state your first name.")]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Please state your last name.")]
        public string LastName { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only digits can appear in a phone number.")]
        [MinLength(9, ErrorMessage = "The phone number must have 9 characters.")]
        [MaxLength(9)]
        public string PrivatePhoneNumber { get; set; }
        [Required(ErrorMessage = "Please provide your business phone number.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only digits can appear in a phone number.")]
        [MinLength(9, ErrorMessage = "The phone number must have 9 characters.")]
        [MaxLength(9)]
        public string BusinessPhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$",
            ErrorMessage = "The password must have at least one lowercase letter, one uppercase letter, one numeric character, " +
            "one special character, and consist of at least 6 characters.")]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please confirm your password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$",
            ErrorMessage = "The password must have at least one lowercase letter, one uppercase letter, one numeric character, " +
            "one special character, and consist of at least 6 characters.")]
        [Compare("Password", ErrorMessage = "Passwords must be the same.")]
        public string ConfirmPassword { get; set; }
        public bool IsEmailVerified { get; set; }
        public Guid ActivationCode { get; set; }
        public string Role { get; set; }

        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}