using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Moon.Validation
{
    /// <summary>
    /// The base class for validators dependent on other properties / fields.
    /// </summary>
    public abstract class DependentAttribute : ValAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependentAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">The property the validator is dependent on.</param>
        protected DependentAttribute(string otherProperty)
        {
            if (string.IsNullOrWhiteSpace(otherProperty))
            {
                throw new ArgumentException("otherProperty is null or empty", nameof(otherProperty));
            }

            OtherProperty = otherProperty;
        }

        /// <summary>
        /// Gets the property the validator is dependent on.
        /// </summary>
        public string OtherProperty { get; }

        /// <summary>
        /// Gets or sets the display name of the other property.
        /// </summary>
        public string OtherPropertyDisplayName { get; set; }

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

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name,
                OtherPropertyDisplayName ?? OtherProperty);
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        protected override sealed bool IsValidValue(object value, ValidationContext validationContext)
            => IsValidValue(value, GetOtherPropertyValue(validationContext), validationContext);

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="otherValue">The value of the property the validator is dependent on.</param>
        /// <param name="validationContext">The validation context.</param>
        protected abstract bool IsValidValue(object value, object otherValue, ValidationContext validationContext);
        
        object GetOtherPropertyValue(ValidationContext validationContext)
        {
            var currentType = validationContext.ObjectType;
            var value = validationContext.ObjectInstance;

            foreach (var propertyName in OtherProperty.Split('.'))
            {
                var property = currentType.GetRuntimeProperty(propertyName);

                if (property == null)
                {
                    var message = $"A property '{propertyName}' could not be found on type '{value.GetType().FullName}'.";
                    throw new Exception(message);
                }

                value = property.GetValue(value, null);
                currentType = property.PropertyType;
            }

            return value;
        }
    }
}