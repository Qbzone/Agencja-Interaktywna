using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("pracownik")]
    public partial class Pracownik
    {
        public Pracownik()
        {
            Pracownikklient = new HashSet<Pracownikklient>();
            Pracownikumowa = new HashSet<Pracownikumowa>();
            Pracownikzespol = new HashSet<Pracownikzespol>();
        }

        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Required]
        [Column("adreszamieszkania")]
        [StringLength(100)]
        public string Adreszamieszkania { get; set; }
        public int Pensja { get; set; }
        public int? Premia { get; set; }
        [Required]
        [Column("PESEL")]
        [StringLength(11)]
        public string Pesel { get; set; }
        public int StazPracy { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Osoba.Pracownik))]
        public virtual Osoba IdpracownikNavigation { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual Grafik Grafik { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual Pozycjoner Pozycjoner { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual Programista Programista { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual Szef Szef { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual Tester Tester { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual ICollection<Pracownikklient> Pracownikklient { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual ICollection<Pracownikumowa> Pracownikumowa { get; set; }
        [InverseProperty("IdpracownikNavigation")]
        public virtual ICollection<Pracownikzespol> Pracownikzespol { get; set; }
    }
}
