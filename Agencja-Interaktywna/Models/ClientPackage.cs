using System;
using System.Collections.Generic;

#nullable disable
namespace Interactive_Agency.Models
{
    public partial class ClientPackage
    {
        public int ClientId { get; set; }
        public int PackageId { get; set; }
        public DateTime? CooperationEnd { get; set; }
        public DateTime CooperationStart { get; set; }

        public virtual Client ClientIdNavigation { get; set; }
        public virtual Package PackageIdNavigation { get; set; }
    }
}