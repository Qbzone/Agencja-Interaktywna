using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("jezykprogramowania")]
    public partial class Jezykprogramowania
    {
        public Jezykprogramowania()
        {
            Programistajezyk = new HashSet<Programistajezyk>();
        }

        [Key]
        [Column("idjezyk")]
        public int Idjezyk { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }

        [InverseProperty("IdjezykNavigation")]
        public virtual ICollection<Programistajezyk> Programistajezyk { get; set; }
    }
}
