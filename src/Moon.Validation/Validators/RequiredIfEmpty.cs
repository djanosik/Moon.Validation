using System;
using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is required when the other property value is empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfEmpty : DependentValidator, IValidatorNameProvider
    {
        readonly RequiredAttribute reqValidator = new RequiredAttribute();

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfEmpty" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public RequiredIfEmpty(string otherProperty)
            : base(otherProperty)
        {
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name of the validated property.</param>
        public override string FormatErrorMessage(string name)
        {
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
        /// <param name="context">The validation context.</param>
        protected override bool IsValidValue(object value, object otherValue, ValidationContext context)
        {
            if (IsEmpty(otherValue))
            {
                var result = reqValidator.GetValidationResult(value, context);
                return result == null || result == ValidationResult.Success;
            }
            return true;
        }

        bool IsEmpty(object otherValue)
        {
            var strValue = otherValue as string;

            if (strValue != null)
            {
                return string.IsNullOrWhiteSpace(strValue);
            }

            return otherValue == null;
        }

        void UpdateInnerValidator()
        {
            reqValidator.ErrorMessage = ErrorMessage;
            reqValidator.ErrorMessageResourceName = ErrorMessageResourceName;
            reqValidator.ErrorMessageResourceType = ErrorMessageResourceType;
        }
    }
}