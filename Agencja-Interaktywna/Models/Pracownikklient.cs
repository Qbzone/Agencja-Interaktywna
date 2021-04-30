using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agencja_Interaktywna.Models
{
    public partial class PracownikKlient : IValidatableObject
    {
        [Required(ErrorMessage = "Proszę wybrać pracownika.")]
        public int? IdPracownik { get; set; }

        [Required(ErrorMessage = "Proszę wybrać klienta.")]
        public int? IdKlient { get; set; }

        [DataType(DataType.DateTime), Required(ErrorMessage = "Proszę wprowadzić datę rozpoczęcia spotkania.")]
        public DateTime? DataRozpoczeciaSpotkania { get; set; }
        
        [DataType(DataType.DateTime), Required(ErrorMessage = "Proszę wprowadzić datę zakończenia spotkania.")]
        public DateTime? DataZakonczeniaSpotkania { get; set; }
        
        public string MiejsceSpotkania { get; set; }

        public virtual Klient IdKlientNavigation { get; set; }
        public virtual Pracownik IdPracownikNavigation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (MiejsceSpotkania == null)
            {
                errors.Add(new ValidationResult($"Miejsce spotkania nie może być puste.", new List<string> { nameof(MiejsceSpotkania) }));
            }
            if (DataRozpoczeciaSpotkania > DataZakonczeniaSpotkania)
            {
                errors.Add(new ValidationResult($"Data rozpoczęcia spotkania musi być mniejsza niż data zakończenia spotkania.", new List<string> { nameof(DataRozpoczeciaSpotkania) }));
            }
            return errors;
        }
    }
}
