using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interactive_Agency.Models
{
    public partial class EmployeeClient : IValidatableObject
    {
        public int? EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public DateTime? MeetingStart { get; set; }
        public DateTime? MeetingEnd { get; set; }
        public string MeetingLocation { get; set; }
        public virtual Client ClientIdNavigation { get; set; }
        public virtual Employee EmployeeIdNavigation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (EmployeeId == null)
            {
                errors.Add(new ValidationResult($"Please select an employee.", new List<string> { nameof(EmployeeId) }));
            }
            if (ClientId == null)
            {
                errors.Add(new ValidationResult($"Please select a client.", new List<string> { nameof(ClientId) }));
            }
            if (MeetingStart == null)
            {
                errors.Add(new ValidationResult($"Please enter the start date of the meeting.", new List<string> { nameof(MeetingStart) }));
            }
            if (MeetingEnd == null)
            {
                errors.Add(new ValidationResult($"Please enter the end date of the meeting.", new List<string> { nameof(MeetingEnd) }));
            }
            if (MeetingStart > MeetingEnd)
            {
                errors.Add(new ValidationResult($"The start date of the meeting must be earlier than the end date of the meeting.", new List<string> { nameof(MeetingStart) }));
            }
            if (MeetingLocation == null)
            {
                errors.Add(new ValidationResult($"Please enter the meeting place.", new List<string> { nameof(MeetingLocation) }));
            }

            return errors;
        }
    }
}