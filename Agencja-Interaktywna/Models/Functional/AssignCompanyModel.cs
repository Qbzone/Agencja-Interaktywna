using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class AssignCompanyModel : IValidatableObject
    {
        public Client Client { get; set; }
        public Company Company { get; set; }
        [NotMapped]
        public string ErrorHandler { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Company.CompanyName == null)
            {
                errors.Add(new ValidationResult($"Please enter your company name.", new List<string> { nameof(ErrorHandler) }));
            }

            return errors;
        }
    }
}