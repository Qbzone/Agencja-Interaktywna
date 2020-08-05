using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("zespol")]
    public partial class Zespol
    {
        public Zespol()
        {
            Pracownikzespol = new HashSet<Pracownikzespol>();
            Zespolprojekt = new HashSet<Zespolprojekt>();
        }

        [Key]
        [Column("idzespol")]
        public int Idzespol { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }

        [InverseProperty("IdzespolNavigation")]
        public virtual ICollection<Pracownikzespol> Pracownikzespol { get; set; }
        [InverseProperty("IdzespolNavigation")]
        public virtual ICollection<Zespolprojekt> Zespolprojekt { get; set; }
    }
}
