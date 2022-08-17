using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class EmployeeContract
    {
        public int EmployeeId { get; set; }
        public int ContractId { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }

        public virtual Employee EmployeeIdNavigation { get; set; }
        public virtual Contract ContractIdNavigation { get; set; }
    }
}