using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class PersonForgottenPassword
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$",
            ErrorMessage = "The password must have at least one lowercase letter, one uppercase letter, one numeric character, " +
            "one special character, and consist of at least 6 characters.")]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please confirm your password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$",
            ErrorMessage = "The password must have at least one lowercase letter, one uppercase letter, one numeric character, " +
            "one special character, and consist of at least 6 characters.")]
        [Compare("Haslo", ErrorMessage = "Hasła muszą być takie same")]
        public string ConfirmPassword { get; set; }
    }
}