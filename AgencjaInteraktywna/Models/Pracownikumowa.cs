using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("pracownikumowa")]
    public partial class Pracownikumowa
    {
        [Key]
        [Column("idpracownik")]
        public int Idpracownik { get; set; }
        [Key]
        [Column("idumowa")]
        public int Idumowa { get; set; }
        [Key]
        [Column("datapodpisaniaumowy", TypeName = "date")]
        public DateTime Datapodpisaniaumowy { get; set; }
        [Column("datawygasnieciaumowy", TypeName = "date")]
        public DateTime Datawygasnieciaumowy { get; set; }

        [ForeignKey(nameof(Idpracownik))]
        [InverseProperty(nameof(Pracownik.Pracownikumowa))]
        public virtual Pracownik IdpracownikNavigation { get; set; }
        [ForeignKey(nameof(Idumowa))]
        [InverseProperty(nameof(Umowa.Pracownikumowa))]
        public virtual Umowa IdumowaNavigation { get; set; }
    }
}
