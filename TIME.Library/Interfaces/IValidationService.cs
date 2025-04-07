using System.ComponentModel.DataAnnotations;

namespace TIME.Library.Interfaces;

public interface IValidationService
{
    bool ValidateProperty(object value, ValidationContext context, out ICollection<ValidationResult> results);
}
