using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Interactive_Agency.Models.Functional
{
    public class TeamCreateModel : IValidatableObject
    {
        public Team Zespol { get; set; }
        public Employee Pracownik { get; set; }
        public IList<SelectListItem> Pracowniks { get; set; }

        [NotMapped]
        public string ErrorHandler1 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Zespol.Nazwa == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić nazwę zespołu.", new List<string> { nameof(ErrorHandler1) }));
            }
            if (Pracowniks.Where(x => x.Selected).Select(y => y.Value).Count() == 0)
            {
                errors.Add(new ValidationResult($"Proszę wybrać conajmniej jednego pracownika.", new List<string> { nameof(Pracowniks) }));
            }

            return errors;
        }
    }
}