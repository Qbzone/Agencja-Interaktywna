using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interactive_Agency.Models
{
    public partial class ServiceProject : IValidatableObject
    {
        public int ProjectId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime? AssignStart { get; set; }
        public DateTime? AssignEnd { get; set; }
        public string Status { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public virtual Project ProjectIdNavigation { get; set; }
        public virtual Service ServiceIdNavigation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (ServiceId == null)
            {
                errors.Add(new ValidationResult($"Please select a task.", new List<string> { nameof(ServiceId) }));
            }
            if (Description == null)
            {
                errors.Add(new ValidationResult($"Please enter a description of the task.", new List<string> { nameof(Description) }));
            }
            if (AssignStart == null)
            {
                errors.Add(new ValidationResult($"Please enter the assignment date.", new List<string> { nameof(AssignStart) }));
            }
            if (AssignEnd == null)
            {
                errors.Add(new ValidationResult($"Please enter the completion date.", new List<string> { nameof(AssignEnd) }));
            }
            if (AssignStart > AssignEnd)
            {
                errors.Add(new ValidationResult($"The assignment date must be earlier than the assignment completion date.", new List<string> { nameof(AssignStart) }));
            }

            return errors;
        }
    }
}