using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agencja_Interaktywna.Models
{
    public partial class PracownikKlient : IValidatableObject
    {
        public int? IdPracownik { get; set; }
        public int? IdKlient { get; set; }
        public DateTime? DataRozpoczeciaSpotkania { get; set; }
        public DateTime? DataZakonczeniaSpotkania { get; set; }
        public string MiejsceSpotkania { get; set; }
        public virtual Klient IdKlientNavigation { get; set; }
        public virtual Pracownik IdPracownikNavigation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (IdPracownik == null)
            {
                errors.Add(new ValidationResult($"Proszę wybrać pracownika.", new List<string> { nameof(IdPracownik) }));
            }
            if (IdKlient == null)
            {
                errors.Add(new ValidationResult($"Proszę wybrać klienta.", new List<string> { nameof(IdKlient) }));
            }
            if (MiejsceSpotkania == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić miejsce spotkania.", new List<string> { nameof(MiejsceSpotkania) }));
            }
            if (DataRozpoczeciaSpotkania == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić datę rozpoczęcia spotkania.", new List<string> { nameof(DataRozpoczeciaSpotkania) }));
            }
            if (DataZakonczeniaSpotkania == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić datę zakończenia spotkania.", new List<string> { nameof(DataZakonczeniaSpotkania) }));
            }
            if (DataRozpoczeciaSpotkania > DataZakonczeniaSpotkania)
            {
                errors.Add(new ValidationResult($"Data rozpoczęcia spotkania musi być mniejsza niż data zakończenia spotkania.", new List<string> { nameof(DataRozpoczeciaSpotkania) }));
            }

            return errors;
        }
    }
}
