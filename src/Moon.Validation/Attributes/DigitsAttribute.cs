using System;
using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value should contain only digits.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DigitsAttribute : TypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitsAttribute" /> class.
        /// </summary>
        public DigitsAttribute()
            : base("Digits")
        {
        }

        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => "The field {0} should contain only digits.";

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

            long result;
            var success = long.TryParse(Convert.ToString(value), out result);
            return success && result >= 0;
        }
    }
}