using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Moon.Validation
{
    /// <summary>Provides a way for an object to be invalidated.</summary>
    public interface IAsyncValidatableObject
    {
        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext);
    }
}