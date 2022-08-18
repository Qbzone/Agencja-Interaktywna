using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class TeamEditModel : IValidatableObject
    {
        public Team Team { get; set; }
        public Employee Employee { get; set; }
        public List<CheckBoxItem> Employees { get; set; }

        [NotMapped]
        public string ErrorHandler1 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Team.TeamName == null)
            {
                errors.Add(new ValidationResult($"Please enter the name of the team.", new List<string> { nameof(ErrorHandler1) }));
            }
            if (Employees.Where(x => x.IsChecked).Count() == 0)
            {
                errors.Add(new ValidationResult($"Please select at least one employee.", new List<string> { nameof(Employees) }));
            }

            return errors;
        }
    }
}