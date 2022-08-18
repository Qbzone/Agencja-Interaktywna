using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class TeamProject
    {
        public int TeamId { get; set; }
        public int ProjectId { get; set; }
        public DateTime AssignStart { get; set; }
        public DateTime? AssignEnd { get; set; }

        public virtual Project ProjectIdNavigation { get; set; }
        public virtual Team TeamIdNavigation { get; set; }
    }
}