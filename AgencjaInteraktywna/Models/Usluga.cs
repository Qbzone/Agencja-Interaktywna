using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("usluga")]
    public partial class Usluga
    {
        public Usluga()
        {
            Pakietusluga = new HashSet<Pakietusluga>();
        }

        [Key]
        [Column("idusluga")]
        public int Idusluga { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }
        [Column("opis")]
        [StringLength(100)]
        public string Opis { get; set; }

        [InverseProperty("IduslugaNavigation")]
        public virtual ICollection<Pakietusluga> Pakietusluga { get; set; }
    }
}
