using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class ProgrammerLanguage
    {
        public int EmployeeId { get; set; }
        public int LanguageId { get; set; }
        public int KnowledgeLevel { get; set; }

        public virtual ProgrammingLanguage LanguageIdNavigation { get; set; }
        public virtual Programmer EmployeeIdNavigation { get; set; }
    }
}