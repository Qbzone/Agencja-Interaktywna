using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interactive_Agency.Models
{
    public partial class Team
    {
        public Team()
        {
            EmployeeTeam = new HashSet<EmployeeTeam>();
            TeamProject = new HashSet<TeamProject>();
        }

        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        [NotMapped]
        public string View { get; set; }
        [NotMapped]
        public int MembersCount { get; set; }
        [NotMapped]
        public int ProjectsCount { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeam { get; set; }
        public virtual ICollection<TeamProject> TeamProject { get; set; }
    }
}