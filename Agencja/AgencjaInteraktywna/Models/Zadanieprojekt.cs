using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("zadanieprojekt")]
    public partial class Zadanieprojekt
    {
        [Key]
        [Column("idzadanie")]
        public int Idzadanie { get; set; }
        [Key]
        [Column("idprojekt")]
        public int Idprojekt { get; set; }
        [Key]
        [Column("datarozpoczeciazadania", TypeName = "datetime")]
        public DateTime Datarozpoczeciazadania { get; set; }
        [Column("datazakonczeniazadania", TypeName = "datetime")]
        public DateTime? Datazakonczeniazadania { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string Status { get; set; }
        [Column("opis")]
        [StringLength(100)]
        public string Opis { get; set; }

        [ForeignKey(nameof(Idprojekt))]
        [InverseProperty(nameof(Projekt.Zadanieprojekt))]
        public virtual Projekt IdprojektNavigation { get; set; }
        [ForeignKey(nameof(Idzadanie))]
        [InverseProperty(nameof(Zadanie.Zadanieprojekt))]
        public virtual Zadanie IdzadanieNavigation { get; set; }
    }
}
