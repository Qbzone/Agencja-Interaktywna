using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Interactive_Agency.Models.Functional
{
    public class TeamCreateModel
    {
        public Team Team { get; set; }
        public Employee Employee { get; set; }
        public IList<SelectListItem> Employees { get; set; }

        [NotMapped]
        public string TeamErrorHandler { get; set; }
    }
}