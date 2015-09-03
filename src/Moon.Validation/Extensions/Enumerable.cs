using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moon.Validation
{
    /// <summary>
    /// <see cref="IEnumerable{T}" /> extension methods
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Throws a <see cref="ValidationException" /> if the enumeration contains any error results.
        /// </summary>
        /// <param name="results">The enumeration of validation results.</param>
        public static void ThrowException(this IEnumerable<ValidationResult> results)
        {
            var errorResult = results.FirstOrDefault(x => x != ValidationResult.Success);

            if (errorResult != null)
            {
                throw new ValidationException(errorResult, null, null);
            }
        }
    }
}