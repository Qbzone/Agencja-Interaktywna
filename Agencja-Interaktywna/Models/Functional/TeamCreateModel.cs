using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Agencja_Interaktywna.Models.Functional
{
    public class TeamCreateModel : IValidatableObject
    {
        public Zespol zespol { get; set; }
        public Pracownik pracownik { get; set; }
        public IList<SelectListItem> pracowniks { get; set; }

        [NotMapped]
        public string ErrorHandler1 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (zespol.Nazwa == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić nazwę zespołu.", new List<string> { nameof(ErrorHandler1) }));
            }
            if (pracowniks.Where(x => x.Selected).Select(y => y.Value).Count() == 0)
            {
                errors.Add(new ValidationResult($"Proszę wybrać conajmniej jednego pracownika.", new List<string> { nameof(pracowniks) }));
            }

            return errors;
        }
    }
}
