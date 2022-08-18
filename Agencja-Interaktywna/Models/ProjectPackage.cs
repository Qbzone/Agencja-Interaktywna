using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class ProjectPackage
    {
        public int ProjectId { get; set; }
        public int PackageId { get; set; }
        public DateTime DealStart { get; set; }
        public DateTime? DealEnd { get; set; }

        public virtual Package PackageIdNavigation { get; set; }
        public virtual Project ProjectIdNavigation { get; set; }
    }
}