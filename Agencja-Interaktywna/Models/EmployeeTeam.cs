using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interactive_Agency.Models
{
    public partial class EmployeeTeam
    {
        public int EmployeeId { get; set; }
        public int TeamId { get; set; }
        public DateTime AssignStart { get; set; }
        public DateTime? AssignEnd { get; set; }

        public virtual Employee EmployeeIdNavigation { get; set; }
        public virtual Team TeamIdNavigation { get; set; }
    }
}