using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interactive_Agency.Models
{
    public partial class Package
    {
        public Package()
        {
            PackageService = new HashSet<PackageService>();
            ProjectPackage = new HashSet<ProjectPackage>();
        }

        public int? PackageId { get; set; }
        public string PackageName { get; set; }
        public int Fee { get; set; }
        public string FeeType { get; set; }

        public virtual ICollection<PackageService> PackageService { get; set; }
        public virtual ICollection<ProjectPackage> ProjectPackage { get; set; }
    }
}