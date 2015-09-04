using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies the name of an additional type to associate with a data field.
    /// </summary>
    public abstract class TypeAttribute : DataTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTypeValidator" /> class.
        /// </summary>
        /// <param name="type">The data type.</param>
        protected TypeAttribute(DataType type)
            : base(type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTypeValidator" /> class.
        /// </summary>
        /// <param name="customType">The custom data type.</param>
        protected TypeAttribute(string customType)
            : base(customType)
        {
        }

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
        /// Validates the specified value and returns a result.
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