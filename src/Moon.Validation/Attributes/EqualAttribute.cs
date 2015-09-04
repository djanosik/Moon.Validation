using System;
using Moon.Validation.Operators;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value is valid when it is equal to the other property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EqualAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        public EqualAttribute(string otherProperty)
            : base(Ops.Equal, otherProperty)
        {
        }
    }
}