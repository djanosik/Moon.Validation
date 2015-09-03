using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is required when the other property value is greater than
    /// or equal to the target value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfGreaterThanOrEqual : RequiredIfValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfGreaterThanOrEqual" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        /// <param name="targetValue">The target value of the property.</param>
        public RequiredIfGreaterThanOrEqual(string otherProperty, object targetValue)
            : base(Ops.GreaterThanOrEqual, otherProperty, targetValue)
        {
        }
    }
}