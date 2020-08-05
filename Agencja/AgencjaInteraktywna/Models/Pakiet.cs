using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("pakiet")]
    public partial class Pakiet
    {
        public Pakiet()
        {
            Klientpakiet = new HashSet<Klientpakiet>();
            Pakietusluga = new HashSet<Pakietusluga>();
        }

        [Key]
        [Column("idpakiet")]
        public int Idpakiet { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }
        public int Oplata { get; set; }
        [Required]
        [StringLength(50)]
        public string RodzajOplaty { get; set; }

        [InverseProperty("IdpakietNavigation")]
        public virtual ICollection<Klientpakiet> Klientpakiet { get; set; }
        [InverseProperty("IdpakietNavigation")]
        public virtual ICollection<Pakietusluga> Pakietusluga { get; set; }
    }
}
