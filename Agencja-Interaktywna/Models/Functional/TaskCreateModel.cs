using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class TaskCreateModel
    {
        public ServiceProject ServiceProject { get; set; }
        public List<Service> Services { get; set; }
        public Project Project { get; set; }
    }
}