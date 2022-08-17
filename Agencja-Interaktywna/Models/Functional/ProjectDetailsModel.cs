using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class ProjectDetailsModel
    {
        public Project projekt { get; set; }
        public List<ServiceProject> zadanies { get; set; }

    }
}