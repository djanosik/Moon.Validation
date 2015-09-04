using System;
using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value must be integer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IntegerAttribute : TypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerAttribute" /> class.
        /// </summary>
        public IntegerAttribute()
            : base("Integer")
        {
        }

        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => "The field {0} should be a positive or negative non-decimal number.";

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        protected override bool IsValidValue(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return true;
            }

            int result;
            return int.TryParse(Convert.ToString(value), out result);
        }
    }
}