using System;
using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value must be a float.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Float : DataTypeValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Float" /> class.
        /// </summary>
        public Float()
            : base("Float")
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
        /// <param name="context">The validation context.</param>
        protected override bool IsValidValue(object value, ValidationContext context)
        {
            if (value == null)
            {
                return true;
            }

            float result;
            return float.TryParse(Convert.ToString(value), out result);
        }
    }
}