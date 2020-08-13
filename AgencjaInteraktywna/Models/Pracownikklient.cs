using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("pracownikklient")]
    public partial class Pracownikklient
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Key]
        [Column("idklient")]
        public int Idklient { get; set; }
        [Key]
        [Column("datarozpoczeciaspotkania", TypeName = "datetime")]
        public DateTime Datarozpoczeciaspotkania { get; set; }
        [Column("datazakonczeniaspotkania", TypeName = "datetime")]
        public DateTime? Datazakonczeniaspotkania { get; set; }
        [Required]
        [Column("miejscespotkania")]
        [StringLength(50)]
        public string Miejscespotkania { get; set; }

        [ForeignKey(nameof(Idklient))]
        [InverseProperty(nameof(Klient.Pracownikklient))]
        public virtual Klient IdklientNavigation { get; set; }
        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Pracownikklient))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
