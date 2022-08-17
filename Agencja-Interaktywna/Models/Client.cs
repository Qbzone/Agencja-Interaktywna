using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientCompany = new HashSet<ClientCompany>();
            EmployeeClient = new HashSet<EmployeeClient>();
        }

        public int ClientId { get; set; }
        public string Priority { get; set; }

        public virtual Person ClientIdNavigation { get; set; }
        public virtual ICollection<ClientCompany> ClientCompany { get; set; }
        public virtual ICollection<EmployeeClient> EmployeeClient { get; set; }
    }
}