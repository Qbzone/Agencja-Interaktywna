﻿using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class ProgrammerLanguage
    {
        public int IdPracownik { get; set; }
        public int IdJezyk { get; set; }
        public int Staz { get; set; }

        public virtual ProgrammingLanguage IdJezykNavigation { get; set; }
        public virtual Programmer IdPracownikNavigation { get; set; }
    }
}