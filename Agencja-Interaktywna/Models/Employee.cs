using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeClient = new HashSet<EmployeeClient>();
            EmployeeContract = new HashSet<EmployeeContract>();
            EmployeeTeam = new HashSet<EmployeeTeam>();
        }

        public int EmployeeId { get; set; }
        public string HomeAddress { get; set; }
        public int Salary { get; set; }
        public int Bonus { get; set; }
        public string PeselNumber { get; set; }
        public int Seniority { get; set; }

        public virtual Person EmployeeIdNavigation { get; set; }
        public virtual Graphician Graphician { get; set; }
        public virtual Positioner Positioner { get; set; }
        public virtual Programmer Programmer { get; set; }
        public virtual Boss Boss { get; set; }
        public virtual Tester Tester { get; set; }
        public virtual ICollection<EmployeeClient> EmployeeClient { get; set; }
        public virtual ICollection<EmployeeContract> EmployeeContract { get; set; }
        public virtual ICollection<EmployeeTeam> EmployeeTeam { get; set; }
    }
}