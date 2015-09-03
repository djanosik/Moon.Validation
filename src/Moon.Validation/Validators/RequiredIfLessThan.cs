using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is required when the other property value is less than the
    /// target value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfLessThan : RequiredIfValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfLessThan" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        /// <param name="targetValue">The target value of the property.</param>
        public RequiredIfLessThan(string otherProperty, object targetValue)
            : base(Ops.LessThan, otherProperty, targetValue)
        {
        }
    }
}