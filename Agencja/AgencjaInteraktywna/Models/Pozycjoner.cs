using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("pozycjoner")]
    public partial class Pozycjoner
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Required]
        [Column("pelnionafunkcja")]
        [StringLength(50)]
        public string Pelnionafunkcja { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Pozycjoner))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
