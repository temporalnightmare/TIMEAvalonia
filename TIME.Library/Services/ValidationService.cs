using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TIME.Library.Interfaces;

namespace TIME.Library.Services;

public class ValidationService : IValidationService
{
    private readonly Dictionary<string, ValidationRule> _validationRules;

    public ValidationService()
    {
        _validationRules = new Dictionary<string, ValidationRule>
        {
            // String Editor
            ["Description"] = new ValidationRule
            {
                Required = true,
                MinLength = 1,
                ErrorMessage = "Description must be entered at a minimum of 1 character"
            }

            // Add more rules as needed
        };
    }

    public bool ValidateProperty(object value, ValidationContext context, out ICollection<ValidationResult> results)
    {
        results = new List<ValidationResult>();

        if (!_validationRules.TryGetValue(context.MemberName, out var rule))
            return true;

        if (rule.Required && (value == null || string.IsNullOrWhiteSpace(value.ToString())))
        {
            results.Add(new ValidationResult(rule.ErrorMessage, new[] { context.MemberName }));
            return false;
        }

        var stringValue = value?.ToString() ?? string.Empty;


        // Custom validation if provided
        if (rule.Validate != null)
        {
            var customError = rule.Validate(value);
            if (customError != null)
            {
                results.Add(new ValidationResult(customError, new[] { context.MemberName }));
                return false;
            }
        }

        if (rule.MinLength.HasValue && stringValue.Length < rule.MinLength)
        {
            results.Add(new ValidationResult(rule.ErrorMessage, new[] { context.MemberName }));
            return false;
        }

        if (rule.MaxLength.HasValue && stringValue.Length > rule.MaxLength)
        {
            results.Add(new ValidationResult(rule.ErrorMessage, new[] { context.MemberName }));
            return false;
        }

        if (!string.IsNullOrEmpty(rule.Pattern) && !Regex.IsMatch(stringValue, rule.Pattern))
        {
            results.Add(new ValidationResult(rule.ErrorMessage, new[] { context.MemberName }));
            return false;
        }

        return true;
    }
}

public class ValidationRule
{
    public bool Required { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public string Pattern { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public Func<object, string> Validate { get; set; }
}