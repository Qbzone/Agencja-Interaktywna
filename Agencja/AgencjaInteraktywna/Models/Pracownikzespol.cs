using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("pracownikzespol")]
    public partial class Pracownikzespol
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Key]
        [Column("idzespol")]
        public int Idzespol { get; set; }
        [Key]
        [Column("dataprzypisaniapracownika", TypeName = "date")]
        public DateTime Dataprzypisaniapracownika { get; set; }
        [Column("datawypisaniapracownika", TypeName = "date")]
        public DateTime? Datawypisaniapracownika { get; set; }
        [Column("menadżer")]
        public bool Menadżer { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Pracownikzespol))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
        [ForeignKey(nameof(Idzespol))]
        [InverseProperty(nameof(Zespol.Pracownikzespol))]
        public virtual Zespol IdzespolNavigation { get; set; }
    }
}
