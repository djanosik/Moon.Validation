using System;
using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value must be a double.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DoubleAttribute : TypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleAttribute" /> class.
        /// </summary>
        public DoubleAttribute()
            : base("Double")
        {
        }

        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => "The {0} field is not a valid number.";

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

            double result;
            return double.TryParse(Convert.ToString(value), out result);
        }
    }
}