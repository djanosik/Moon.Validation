using System.ComponentModel.DataAnnotations;
using Moon.Validation.Operators;

// ReSharper disable once CheckNamespace

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid if it meets a condition specified by a comparison operator.
    /// </summary>
    public abstract class CompareAttribute : DependentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareAttribute" /> class.
        /// </summary>
        /// <param name="operator">The comparison operator.</param>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        protected CompareAttribute(IOperator @operator, string otherProperty)
            : base(otherProperty)
        {
            Operator = @operator;
        }

        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => Operator.DefaultErrorMessage;

        /// <summary>
        /// Gets the operator used to compare values.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public IOperator Operator { get; }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="otherValue">The value of the property the validator is dependent on.</param>
        /// <param name="validationContext">The validation context.</param>
        protected sealed override bool IsValidValue(object value, object otherValue, ValidationContext validationContext)
        {
            var oneIsNull = value == null || otherValue == null;
            return oneIsNull || Operator.Compare(value, otherValue);
        }
    }
}