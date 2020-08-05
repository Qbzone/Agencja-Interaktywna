using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("projekt")]
    public partial class Projekt
    {
        public Projekt()
        {
            Zadanieprojekt = new HashSet<Zadanieprojekt>();
            Zespolprojekt = new HashSet<Zespolprojekt>();
        }

        [Key]
        [Column("idprojekt")]
        public int Idprojekt { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }
        [Column("Firma_IdFirma")]
        public int FirmaIdFirma { get; set; }

        [ForeignKey(nameof(FirmaIdFirma))]
        [InverseProperty(nameof(Firma.Projekt))]
        public virtual Firma FirmaIdFirmaNavigation { get; set; }
        [InverseProperty("IdprojektNavigation")]
        public virtual ICollection<Zadanieprojekt> Zadanieprojekt { get; set; }
        [InverseProperty("IdprojektNavigation")]
        public virtual ICollection<Zespolprojekt> Zespolprojekt { get; set; }
    }
}
