using System.ComponentModel.DataAnnotations;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is required when the other property value meets a
    /// condition specified by a comparison operator.
    /// </summary>
    public abstract class RequiredIfValidator : DependentValidator, IValidatorNameProvider
    {
        readonly RequiredAttribute reqValidator = new RequiredAttribute();

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfValidator" /> class.
        /// </summary>
        /// <param name="operator">The comparison operator.</param>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        /// <param name="targetValue">The value that will be compared to the value of the <paramref name="otherProperty" />.</param>
        protected RequiredIfValidator(IOperator @operator, string otherProperty, object targetValue)
            : base(otherProperty)
        {
            Operator = @operator;
            TargetValue = targetValue;
        }

        /// <summary>
        /// Gets the operator used to compare values.
        /// </summary>
        public IOperator Operator { get; }

        /// <summary>
        /// Gets the value that will be compared to the value of the <see cref="DependentValidator.OtherProperty" />.
        /// </summary>
        public object TargetValue { get; private set; }

        /// <summary>
        /// Gets the name of the validator.
        /// </summary>
        public string GetValidatorName()
            => "Required";

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
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="otherValue">The value of the property the validator is dependent on.</param>
        /// <param name="context">The validation context.</param>
        protected override sealed bool IsValidValue(object value, object otherValue, ValidationContext context)
        {
            if (Operator.Compare(otherValue, TargetValue))
            {
                var result = reqValidator.GetValidationResult(value, context);
                return result == null || result == ValidationResult.Success;
            }
            return true;
        }

        void UpdateInnerValidator()
        {
            reqValidator.ErrorMessage = ErrorMessage;
            reqValidator.ErrorMessageResourceName = ErrorMessageResourceName;
            reqValidator.ErrorMessageResourceType = ErrorMessageResourceType;
        }
    }
}