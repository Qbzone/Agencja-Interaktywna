using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Agencja_Interaktywna.Models.Functional
{
    public class TeamCreateModel
    {
        public Zespol zespol { get; set; }
        public Pracownik pracownik { get; set; }
        public IList<SelectListItem> pracowniks { get; set; }
    }
}
