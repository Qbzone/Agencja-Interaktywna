using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Company
    {
        public Company()
        {
            ClientCompany = new HashSet<ClientCompany>();
            Project = new HashSet<Project>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<ClientCompany> ClientCompany { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}