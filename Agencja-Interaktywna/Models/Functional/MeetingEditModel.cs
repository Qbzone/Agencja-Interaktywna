using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class MeetingEditModel
    {
        public EmployeeClient EmployeeClient { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public DateTime MeetingStart { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Client> Clients { get; set; }
    }
}