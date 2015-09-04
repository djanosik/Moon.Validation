using System.Collections.Generic;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="MaxAttribute" /> attribute.
    /// </summary>
    public class MaxAttributeAdapter : AttributeAdapter<MaxAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAttributeAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        public MaxAttributeAdapter(ModelMetadata metadata, ControllerContext context, MaxAttribute attribute)
            : base(metadata, context, attribute)
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