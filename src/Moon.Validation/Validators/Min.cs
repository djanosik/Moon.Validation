﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Moon.Validation
{
    /// <summary>
    /// Specifies that a data field value must be grater than or equal to a specified value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Min : DataTypeValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Min" /> class.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        public Min(int minValue)
            : this((double)minValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Min" /> class.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        public Min(double minValue)
            : base("Number")
        {
            MinValue = minValue;
        }

        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public override string DefaultErrorMessage
            => "The field {0} must be greater than or equal to {1}.";

        /// <summary>
        /// Gets the minimal value.
        /// </summary>
        public double MinValue { get; }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name of the validated property.</param>
        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = DefaultErrorMessage;
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, MinValue);
        }

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

            double valueAsDouble;
            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);
            return isDouble && valueAsDouble >= MinValue;
        }
    }
}