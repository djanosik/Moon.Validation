using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is not equal to the other property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEqualAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public NotEqualAttribute(string otherProperty)
            : base(Ops.NotEqual, otherProperty)
        {
        }
    }
}