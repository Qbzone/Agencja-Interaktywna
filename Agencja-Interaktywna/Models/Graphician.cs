using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Graphician
    {
        public int EmployeeId { get; set; }
        public string Specialization { get; set; }

        public virtual Employee EmployeeIdNavigation { get; set; }
    }
}