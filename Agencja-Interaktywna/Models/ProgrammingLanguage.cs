using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class ProgrammingLanguage
    {
        public ProgrammingLanguage()
        {
            ProgrammerLanguage = new HashSet<ProgrammerLanguage>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public virtual ICollection<ProgrammerLanguage> ProgrammerLanguage { get; set; }
    }
}