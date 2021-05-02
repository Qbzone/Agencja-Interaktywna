using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class ProjectCreateModel : IValidatableObject
    {
        public Projekt projekt { get; set; }
        public Zespol zespol { get; set; }
        public Pakiet pakiet { get; set; }
        public List<Firma> firmas { get; set; }
        public List<Zespol> zespols { get; set; }
        public List<Pakiet> pakiets { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
        [NotMapped]
        public string ErrorHandler1 { get; set; }
        [NotMapped]
        public string ErrorHandler2 { get; set; }
        [NotMapped]
        public string ErrorHandler3 { get; set; }
        [NotMapped]
        public string ErrorHandler4 { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (projekt.Nazwa == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić nazwę projektu.", new List<string> { nameof(ErrorHandler1) }));
            }
            if (FormFile == null)
            {
                errors.Add(new ValidationResult($"Proszę wybrać logo projektu.", new List<string> { nameof(FormFile) }));
            }
            if (projekt.IdFirma == null)
            {
                errors.Add(new ValidationResult($"Proszę wybrać klienta.", new List<string> { nameof(ErrorHandler2) }));
            }
            if (pakiet.IdPakiet == null)
            {
                errors.Add(new ValidationResult($"Proszę wybrać pakiet.", new List<string> { nameof(ErrorHandler3) }));
            }
            if (zespol.IdZespol == null)
            {
                errors.Add(new ValidationResult($"Proszę wybrać zespół.", new List<string> { nameof(ErrorHandler4) }));
            }
            return errors;
        }
    }
}