using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("firma")]
    public partial class Firma
    {
        public Firma()
        {
            Firmatag = new HashSet<Firmatag>();
            Klientfirma = new HashSet<Klientfirma>();
            Projekt = new HashSet<Projekt>();
        }

        [Key]
        [Column("idfirma")]
        public int Idfirma { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }

        [InverseProperty("IdfirmaNavigation")]
        public virtual ICollection<Firmatag> Firmatag { get; set; }
        [InverseProperty("IdfirmaNavigation")]
        public virtual ICollection<Klientfirma> Klientfirma { get; set; }
        [InverseProperty("FirmaIdFirmaNavigation")]
        public virtual ICollection<Projekt> Projekt { get; set; }
    }
}
