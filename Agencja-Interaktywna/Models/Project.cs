using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interactive_Agency.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectPackage = new HashSet<ProjectPackage>();
            ServiceProject = new HashSet<ServiceProject>();
            TeamProject = new HashSet<TeamProject>();
        }

        public int ProjectId { get; set; }
        [MaxLength(50)]
        public string ProjectName { get; set; }
        public string ProjectLogo { get; set; }
        public int? CompanyId { get; set; }
        [NotMapped]
        public string View { get; set; }

        public virtual Company CompanyIdNavigation { get; set; }
        public virtual ICollection<ProjectPackage> ProjectPackage { get; set; }
        public virtual ICollection<ServiceProject> ServiceProject { get; set; }
        public virtual ICollection<TeamProject> TeamProject { get; set; }
    }
}