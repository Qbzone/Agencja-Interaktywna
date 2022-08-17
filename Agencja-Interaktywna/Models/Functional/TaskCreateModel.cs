using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive_Agency.Models.Functional
{
    public class TaskCreateModel
    {
        public ServiceProject UslugaProjekt { get; set; }
        public List<Service> Uslugas { get; set; }
        public Project Projekt { get; set; }
    }
}