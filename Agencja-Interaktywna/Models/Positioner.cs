using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Positioner
    {
        public int EmployeeId { get; set; }
        public string FullFunction { get; set; }

        public virtual Employee EmployeeIdNavigation { get; set; }
    }
}