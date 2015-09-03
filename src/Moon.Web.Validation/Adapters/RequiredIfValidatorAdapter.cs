using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validator type.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator.</typeparam>
    public class RequiredIfValidatorAdapter<TValidator> : DependentValidatorAdapter<TValidator>
        where TValidator : RequiredIfValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfValidatorAdapter{TValidator}" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public RequiredIfValidatorAdapter(ModelMetadata metadata, ControllerContext context, TValidator validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
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