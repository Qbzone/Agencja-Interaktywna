using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("klientfirma")]
    public partial class Klientfirma
    {
        [Key]
        [Column("idklient")]
        public int Idklient { get; set; }
        [Key]
        [Column("idfirma")]
        public int Idfirma { get; set; }

        [ForeignKey(nameof(Idfirma))]
        [InverseProperty(nameof(Firma.Klientfirma))]
        public virtual Firma IdfirmaNavigation { get; set; }
        [ForeignKey(nameof(Idklient))]
        [InverseProperty(nameof(Klient.Klientfirma))]
        public virtual Klient IdklientNavigation { get; set; }
    }
}
