using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("programista")]
    public partial class Programista
    {
        public Programista()
        {
            Programistajezyk = new HashSet<Programistajezyk>();
        }

        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Required]
        [Column("poziomzaawansowania")]
        [StringLength(50)]
        public string Poziomzaawansowania { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Programista))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual ICollection<Programistajezyk> Programistajezyk { get; set; }
    }
}
