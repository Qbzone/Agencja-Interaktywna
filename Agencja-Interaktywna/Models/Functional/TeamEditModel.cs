using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class TeamEditModel
    {
        public Team Team { get; set; }
        public Employee Employee { get; set; }
        public List<CheckBoxItem> Employees { get; set; }

        [NotMapped]
        public string TeamErrorHandler { get; set; }
    }
}