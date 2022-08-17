using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Boss
    {
        public int EmployeeId { get; set; }

        public virtual Employee EmployeeIdNavigation { get; set; }
    }
}