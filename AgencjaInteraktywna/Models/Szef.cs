using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("szef")]
    public partial class Szef
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Szef))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
