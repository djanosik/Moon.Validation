using System;
using System.ComponentModel.DataAnnotations;

// ReSharper disable once CheckNamespace

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is required when the other property value is not empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfNotEmptyAttribute : DependentAttribute, IValidatorNameProvider
    {
        private readonly RequiredAttribute reqValidator = new RequiredAttribute();

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfNotEmptyAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public RequiredIfNotEmptyAttribute(string otherProperty)
            : base(otherProperty)
        {
        }

        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => "The {0} field is required.";

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name of the validated property.</param>
        public override string FormatErrorMessage(string name)
        {
            EnsureErrorMessage();
            UpdateInnerValidator();
            return reqValidator.FormatErrorMessage(name);
        }

        /// <summary>
        /// Gets the name of the validator.
        /// </summary>
        public string GetValidatorName()
            => "Required";

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="otherValue">The value of the property the validator is dependent on.</param>
        /// <param name="validationContext">The validation context.</param>
        protected override bool IsValidValue(object value, object otherValue, ValidationContext validationContext)
        {
            if (IsNotEmpty(otherValue))
            {
                var result = reqValidator.GetValidationResult(value, validationContext);
                return result == null || result == ValidationResult.Success;
            }
            return true;
        }

        private bool IsNotEmpty(object otherValue)
        {
            var strValue = otherValue as string;

            if (strValue != null)
            {
                return !string.IsNullOrWhiteSpace(strValue);
            }

            return otherValue != null;
        }

        private void UpdateInnerValidator()
        {
            reqValidator.ErrorMessage = ErrorMessage;
            reqValidator.ErrorMessageResourceName = ErrorMessageResourceName;
            reqValidator.ErrorMessageResourceType = ErrorMessageResourceType;
        }
    }
}