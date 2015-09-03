using System.Collections.Generic;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="Min" /> validator.
    /// </summary>
    public class MinValidatorAdapter : ValidatorAdapter<Min>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public MinValidatorAdapter(ModelMetadata metadata, ControllerContext context, Min validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "range";

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("min", Attribute.MinValue);
            return parameters;
        }
    }
}