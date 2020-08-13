using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("umowa")]
    public partial class Umowa
    {
        public Umowa()
        {
            Pracownikumowa = new HashSet<Pracownikumowa>();
        }

        [Key]
        [Column("idumowa")]
        public int Idumowa { get; set; }
        [Required]
        [Column("rodzajumowy")]
        [StringLength(50)]
        public string Rodzajumowy { get; set; }

        [InverseProperty("IdumowaNavigation")]
        public virtual ICollection<Pracownikumowa> Pracownikumowa { get; set; }
    }
}
