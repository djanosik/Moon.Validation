using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// When implemented, enables custom validation.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Enumerates results of custom validation.
        /// </summary>
        /// <param name="context">Describes the context in which a validation is performed.</param>
        IEnumerable<ValidationResult> Validate(ValidationContext context);
    }
}