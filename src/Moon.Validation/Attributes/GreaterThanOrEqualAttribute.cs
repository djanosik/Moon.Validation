using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is greater than or equal to the other
    /// property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GreaterThanOrEqualAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqualAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public GreaterThanOrEqualAttribute(string otherProperty)
            : base(Ops.GreaterThanOrEqual, otherProperty)
        {
        }
    }
}