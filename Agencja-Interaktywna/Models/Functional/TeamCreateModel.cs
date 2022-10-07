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
        public Team Team { get; set; }
        public Employee Employee { get; set; }
        public IList<SelectListItem> Employees { get; set; }

        [NotMapped]
        public string ErrorHandler1 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Team.TeamName == null || Team.TeamName == "")
            {
                errors.Add(new ValidationResult($"Please select a team.", new List<string> { nameof(ErrorHandler1) }));
            }
            if (Employees.Where(x => x.Selected).Select(y => y.Value).Count() == 0)
            {
                errors.Add(new ValidationResult($"Please select at least one employee.", new List<string> { nameof(Employees) }));
            }

            return errors;
        }
    }
}