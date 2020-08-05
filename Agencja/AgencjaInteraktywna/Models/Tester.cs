using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("tester")]
    public partial class Tester
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Column("testerdoswiadczenie")]
        public int Testerdoswiadczenie { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Tester))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
