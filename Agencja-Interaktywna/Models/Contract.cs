using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Contract
    {
        public Contract()
        {
            EmployeeContract = new HashSet<EmployeeContract>();
        }

        public int ContractId { get; set; }
        public string ContractType { get; set; }

        public virtual ICollection<EmployeeContract> EmployeeContract { get; set; }
    }
}