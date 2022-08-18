using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Tester
    {
        public int EmploueeId { get; set; }
        public int TestingExperience { get; set; }

        public virtual Employee EmployeeIdNavigation { get; set; }
    }
}