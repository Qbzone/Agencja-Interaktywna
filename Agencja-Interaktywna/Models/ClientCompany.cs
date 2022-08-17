using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class ClientCompany
    {
        public int ClientId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company CompanyIdNavigation { get; set; }
        public virtual Client ClientIdNavigation { get; set; }
    }
}