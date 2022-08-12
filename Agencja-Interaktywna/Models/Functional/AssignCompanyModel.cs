using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class AssignCompanyModel : IValidatableObject
    {
        public Klient Klient { get; set; }

        public Firma Firma { get; set; }

        [NotMapped]
        public string ErrorHandler1 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Firma.Nazwa == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić nazwę firmy.", new List<string> { nameof(ErrorHandler1) }));
            }

            return errors;
        }
    }
}