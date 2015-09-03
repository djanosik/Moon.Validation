using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is required when the other property value is less than or
    /// equal to the target value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfLessThanOrEqual : RequiredIfValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfLessThanOrEqual" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        /// <param name="targetValue">The target value of the property.</param>
        public RequiredIfLessThanOrEqual(string otherProperty, object targetValue)
            : base(Ops.LessThanOrEqual, otherProperty, targetValue)
        {
        }
    }
}