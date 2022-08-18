using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Service
    {
        public Service()
        {
            PackageService = new HashSet<PackageService>();
            ServiceProject = new HashSet<ServiceProject>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Classification { get; set; }

        public virtual ICollection<PackageService> PackageService { get; set; }
        public virtual ICollection<ServiceProject> ServiceProject { get; set; }
    }
}