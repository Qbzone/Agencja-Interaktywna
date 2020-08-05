using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("osoba")]
    public partial class Osoba
    {
        [Key]
        [Column("idosoba")]
        public int Idosoba { get; set; }
        [Required]
        [Column("imie")]
        [StringLength(25)]
        public string Imie { get; set; }
        [Required]
        [StringLength(50)]
        public string Nazwisko { get; set; }
        [StringLength(9)]
        public string NumerTelefonuPrywatny { get; set; }
        [Required]
        [StringLength(9)]
        public string NumerTelefonuSłużbowego { get; set; }
        [Required]
        [StringLength(50)]
        public string AdresEmail { get; set; }

        [InverseProperty("IdklientNavigation")]
        public virtual Klient Klient { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual Pracownik Pracownik { get; set; }
    }
}
