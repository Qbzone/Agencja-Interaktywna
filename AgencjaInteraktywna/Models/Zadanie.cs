using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("zadanie")]
    public partial class Zadanie
    {
        public Zadanie()
        {
            Zadanieprojekt = new HashSet<Zadanieprojekt>();
        }

        [Key]
        [Column("idzadanie")]
        public int Idzadanie { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }

        [InverseProperty("IdzadanieNavigation")]
        public virtual ICollection<Zadanieprojekt> Zadanieprojekt { get; set; }
    }
}
