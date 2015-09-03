using System.Collections.Generic;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="Max" /> validator.
    /// </summary>
    public class MaxValidatorAdapter : ValidatorAdapter<Max>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public MaxValidatorAdapter(ModelMetadata metadata, ControllerContext context, Max validator)
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
            parameters.Add("max", Attribute.MaxValue);
            return parameters;
        }
    }
}