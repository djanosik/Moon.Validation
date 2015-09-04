using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// Serves as the base class for data validators.
    /// </summary>
    public abstract class ValAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public virtual string DefaultErrorMessage
            => "The {0} field is invalid.";

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name of the validated property.</param>
        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = DefaultErrorMessage;
            }
            return base.FormatErrorMessage(name);
        }

        /// <summary>
        /// Validates the specified value and returns the result.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        protected override sealed ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;

            if (!IsValidValue(value, validationContext))
            {
                var memberNames = validationContext.MemberName != null ? new[] { validationContext.MemberName } : null;
                result = new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        protected abstract bool IsValidValue(object value, ValidationContext validationContext);
    }
}