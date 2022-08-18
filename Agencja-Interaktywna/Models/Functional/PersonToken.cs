using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class PersonToken
    {
        [Display(Name = "EmailAddress")]
        [Required(ErrorMessage = "Please enter your email address.")]
        public string EmailAddress { get; set; }
    }
}