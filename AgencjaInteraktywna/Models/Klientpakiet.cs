using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("klientpakiet")]
    public partial class Klientpakiet
    {
        [Key]
        [Column("idklient")]
        public int Idklient { get; set; }
        [Key]
        [Column("idpakiet")]
        public int Idpakiet { get; set; }
        [Key]
        [Column("datarozpoczeciawspolpracy", TypeName = "date")]
        public DateTime Datarozpoczeciawspolpracy { get; set; }
        [Column("datazakonczeniawspolpracy", TypeName = "date")]
        public DateTime? Datazakonczeniawspolpracy { get; set; }

        [ForeignKey(nameof(Idklient))]
        [InverseProperty(nameof(Klient.Klientpakiet))]
        public virtual Klient IdklientNavigation { get; set; }
        [ForeignKey(nameof(Idpakiet))]
        [InverseProperty(nameof(Pakiet.Klientpakiet))]
        public virtual Pakiet IdpakietNavigation { get; set; }
    }
}
