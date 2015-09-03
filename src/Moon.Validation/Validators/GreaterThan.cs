using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is greater than the other property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GreaterThan : CompareValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThan" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public GreaterThan(string otherProperty)
            : base(Ops.GreaterThan, otherProperty)
        {
        }
    }
}