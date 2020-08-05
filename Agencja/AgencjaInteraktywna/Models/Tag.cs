using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("tag")]
    public partial class Tag
    {
        public Tag()
        {
            Firmatag = new HashSet<Firmatag>();
        }

        [Key]
        [Column("idtag")]
        public int Idtag { get; set; }
        [Required]
        [Column("nazwa")]
        [StringLength(50)]
        public string Nazwa { get; set; }

        [InverseProperty("IdtagNavigation")]
        public virtual ICollection<Firmatag> Firmatag { get; set; }
    }
}
