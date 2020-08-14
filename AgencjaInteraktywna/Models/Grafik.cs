using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("grafik")]
    public partial class Grafik
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Required]
        [Column("specjalizacja")]
        [StringLength(50)]
        public string Specjalizacja { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Grafik))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
