using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is less than or equal to the other
    /// property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LessThanOrEqualAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanOrEqualAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public LessThanOrEqualAttribute(string otherProperty)
            : base(Ops.LessThanOrEqual, otherProperty)
        {
        }
    }
}