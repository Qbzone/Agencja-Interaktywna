using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class ProjectPackage
    {
        public int IdProjekt { get; set; }
        public int IdPakiet { get; set; }
        public DateTime DataRozpoczeciaWspolpracy { get; set; }
        public DateTime? DataZakonczeniaWspolpracy { get; set; }

        public virtual Package IdPakietNavigation { get; set; }
        public virtual Project IdProjektNavigation { get; set; }
    }
}