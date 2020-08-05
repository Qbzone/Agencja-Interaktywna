using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("zespolprojekt")]
    public partial class Zespolprojekt
    {
        [Key]
        [Column("idzespol")]
        public int Idzespol { get; set; }
        [Key]
        [Column("idprojekt")]
        public int Idprojekt { get; set; }
        [Key]
        [Column("dataprzypisaniazespolu", TypeName = "date")]
        public DateTime Dataprzypisaniazespolu { get; set; }
        [Column("dataoddaniaprojektu", TypeName = "date")]
        public DateTime? Dataoddaniaprojektu { get; set; }

        [ForeignKey(nameof(Idprojekt))]
        [InverseProperty(nameof(Projekt.Zespolprojekt))]
        public virtual Projekt IdprojektNavigation { get; set; }
        [ForeignKey(nameof(Idzespol))]
        [InverseProperty(nameof(Zespol.Zespolprojekt))]
        public virtual Zespol IdzespolNavigation { get; set; }
    }
}
