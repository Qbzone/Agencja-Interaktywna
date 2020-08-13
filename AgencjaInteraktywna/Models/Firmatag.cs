using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("firmatag")]
    public partial class Firmatag
    {
        [Key]
        [Column("idfirma")]
        public int Idfirma { get; set; }
        [Key]
        [Column("idtag")]
        public int Idtag { get; set; }

        [ForeignKey(nameof(Idfirma))]
        [InverseProperty(nameof(Firma.Firmatag))]
        public virtual Firma IdfirmaNavigation { get; set; }
        [ForeignKey(nameof(Idtag))]
        [InverseProperty(nameof(Tag.Firmatag))]
        public virtual Tag IdtagNavigation { get; set; }
    }
}
