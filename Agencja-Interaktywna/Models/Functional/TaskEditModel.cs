﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class TaskEditModel
    {
        public ServiceProject UslugaProjekt { get; set; }
        public List<Service> Uslugas { get; set; }
    }
}