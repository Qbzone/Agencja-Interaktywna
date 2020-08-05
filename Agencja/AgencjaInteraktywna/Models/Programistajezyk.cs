using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("programistajezyk")]
    public partial class Programistajezyk
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Key]
        [Column("idjezyk")]
        public int Idjezyk { get; set; }
        [Column("staz")]
        public int Staz { get; set; }

        [ForeignKey(nameof(Idjezyk))]
        [InverseProperty(nameof(Jezykprogramowania.Programistajezyk))]
        public virtual Jezykprogramowania IdjezykNavigation { get; set; }
        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Programista.Programistajezyk))]
        public virtual Programista IdpracownikNavigation { get; set; }
    }
}
