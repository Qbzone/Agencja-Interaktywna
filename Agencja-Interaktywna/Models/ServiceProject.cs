using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agencja_Interaktywna.Models
{
    public partial class ServiceProject : IValidatableObject
    {
        public int IdProjekt { get; set; }
        public int? IdUsluga { get; set; }
        public DateTime? DataPrzypisaniaZadania { get; set; }
        public DateTime? DataZakonczeniaZadania { get; set; }
        public string Status { get; set; }
        [MaxLength(255)]
        public string Opis { get; set; }
        public virtual Project IdProjektNavigation { get; set; }
        public virtual Service IdUslugaNavigation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (IdUsluga == null)
            {
                errors.Add(new ValidationResult($"Proszę wybrać zadanie.", new List<string> { nameof(IdUsluga) }));
            }
            if (Opis == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić opis zadania.", new List<string> { nameof(Opis) }));
            }
            if (DataPrzypisaniaZadania == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić datę przypisania zadania.", new List<string> { nameof(DataPrzypisaniaZadania) }));
            }
            if (DataZakonczeniaZadania == null)
            {
                errors.Add(new ValidationResult($"Proszę wprowadzić datę zakończenia zadania.", new List<string> { nameof(DataZakonczeniaZadania) }));
            }
            if (DataPrzypisaniaZadania > DataZakonczeniaZadania)
            {
                errors.Add(new ValidationResult($"Data przypisania zadania musi być mniejsza niż data zakończenia zadania.", new List<string> { nameof(DataPrzypisaniaZadania) }));
            }

            return errors;
        }
    }
}