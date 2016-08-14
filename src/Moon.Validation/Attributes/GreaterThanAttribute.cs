using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is greater than the other property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GreaterThanAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public GreaterThanAttribute(string otherProperty)
            : base(Ops.GreaterThan, otherProperty)
        {
        }
    }
}