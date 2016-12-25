using System;
using Moon.Validation.Operators;

// ReSharper disable once CheckNamespace

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is required when the other property value is less than the
    /// target value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfLessThanAttribute : RequiredIfAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfLessThanAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        /// <param name="targetValue">The target value of the property.</param>
        public RequiredIfLessThanAttribute(string otherProperty, object targetValue)
            : base(Ops.LessThan, otherProperty, targetValue)
        {
        }
    }
}