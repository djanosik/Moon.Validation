using System.Collections.Generic;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="MinAttribute" /> attribute.
    /// </summary>
    public class MinAttributeAdapter : AttributeAdapter<MinAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinAttributeAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        public MinAttributeAdapter(ModelMetadata metadata, ControllerContext context, MinAttribute attribute)
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
            parameters.Add("min", Attribute.MinValue);
            return parameters;
        }
    }
}