using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value must be less than or equal to a specified value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxAttribute : TypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAttribute" /> class.
        /// </summary>
        /// <param name="maxValue">The max value.</param>
        public MaxAttribute(int maxValue)
            : this((double)maxValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAttribute" /> class.
        /// </summary>
        /// <param name="maxValue">The max value.</param>
        public MaxAttribute(double maxValue)
            : base("Number")
        {
            MaxValue = maxValue;
        }

        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => "The field {0} must be less than or equal to {1}.";

        /// <summary>
        /// Gets the maximal value.
        /// </summary>
        public double MaxValue { get; }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name of the validated property.</param>
        public override string FormatErrorMessage(string name)
        {
            EnsureErrorMessage();
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, MaxValue);
        }

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

            double valueAsDouble;
            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);
            return isDouble && valueAsDouble <= MaxValue;
        }
    }
}