using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class ProjectEditModel : IValidatableObject
    {
        public Project Project { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
        public Package Package { get; set; }
        public int PackageId { get; set; }
        public List<Company> Companies { get; set; }
        public List<Team> Teams { get; set; }
        public List<Package> Packages { get; set; }

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

            if (Project.ProjectName == null)
            {
                errors.Add(new ValidationResult($"Please enter the name of the project.", new List<string> { nameof(ErrorHandler1) }));
            }
            if (Project.CompanyId == null)
            {
                errors.Add(new ValidationResult($"Please select a customer.", new List<string> { nameof(ErrorHandler2) }));
            }
            if (Package.PackageId == null)
            {
                errors.Add(new ValidationResult($"Please select a package.", new List<string> { nameof(ErrorHandler3) }));
            }
            if (Team.TeamId == null)
            {
                errors.Add(new ValidationResult($"Please select a team.", new List<string> { nameof(ErrorHandler4) }));
            }

            return errors;
        }
    }
}