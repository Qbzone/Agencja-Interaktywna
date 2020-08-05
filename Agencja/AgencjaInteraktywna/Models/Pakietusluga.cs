using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("pakietusluga")]
    public partial class Pakietusluga
    {
        [Key]
        [Column("idpakiet")]
        public int Idpakiet { get; set; }
        [Key]
        [Column("idusluga")]
        public int Idusluga { get; set; }

        [ForeignKey(nameof(Idpakiet))]
        [InverseProperty(nameof(Pakiet.Pakietusluga))]
        public virtual Pakiet IdpakietNavigation { get; set; }
        [ForeignKey(nameof(Idusluga))]
        [InverseProperty(nameof(Usluga.Pakietusluga))]
        public virtual Usluga IduslugaNavigation { get; set; }
    }
}
