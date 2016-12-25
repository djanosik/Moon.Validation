using System;
using System.ComponentModel.DataAnnotations;

// ReSharper disable once CheckNamespace

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is null or empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredEmptyAttribute : ValAttribute
    {
        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => "The field {0} should be empty.";

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        protected override bool IsValidValue(object value, ValidationContext validationContext)
            => string.IsNullOrEmpty(value as string);
    }
}