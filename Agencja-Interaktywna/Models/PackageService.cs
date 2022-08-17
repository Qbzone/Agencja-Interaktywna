using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class PackageService
    {
        public int PackageId { get; set; }
        public int ServiceId { get; set; }

        public virtual Package PackageIdNavigation { get; set; }
        public virtual Service ServiceIdNavigation { get; set; }
    }
}