using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is not equal to the other property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotEqual : CompareValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Equal" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public NotEqual(string otherProperty)
            : base(Ops.NotEqual, otherProperty)
        {
        }
    }
}