using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace Moon.Windows.Validation
{
    /// <summary>
    /// <see cref="IEnumerable{T}" /> extension methods
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Marks the specified element as invalid if the enumeration contains any error results.
        /// Otherwise, marks the element as valid.
        /// </summary>
        /// <param name="results">The enumeration of validation results.</param>
        /// <param name="element">The element to be marked in/valid.</param>
        public static void MarkInvalid(this IEnumerable<ValidationResult> results, DependencyObject element)
        {
            var errorResult = results.FirstOrDefault(x => x != ValidationResult.Success);

            if (errorResult != null)
            {
                WpfValidation.MarkInvalid(element, errorResult.ErrorMessage);
            }
            else
            {
                WpfValidation.ClearInvalid(element);
            }
        }
    }
}