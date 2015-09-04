using System;
using System.Collections.Generic;
using System.Globalization;
using Moon.Validation;

namespace Moon.AspNet.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validator type.
    /// </summary>
    /// <typeparam name="TAttribute">The validation attribute.</typeparam>
    public class RequiredIfAttributeAdapter<TAttribute> : DependentAttributeAdapter<TAttribute>
        where TAttribute : RequiredIfAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttributeAdapter{TAttribute}" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        public RequiredIfAttributeAdapter(TAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "requiredif";

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("operator", Attribute.Operator.Name);

            var targetValue = string.Empty;

            if (Attribute.TargetValue != null)
            {
                targetValue = Convert.ToString(Attribute.TargetValue, new CultureInfo("en-US"));

                if (Attribute.TargetValue is bool)
                {
                    // if it's a bool, format it javascript style (the default is True or False!)
                    targetValue = targetValue.ToLower();
                }
            }

            parameters.Add("targetvalue", targetValue);
            return parameters;
        }
    }
}