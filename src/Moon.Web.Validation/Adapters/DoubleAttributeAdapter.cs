using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="DoubleAttribute" /> attribute.
    /// </summary>
    public class DoubleAttributeAdapter : AttributeAdapter<DoubleAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleAttributeAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        public DoubleAttributeAdapter(ModelMetadata metadata, ControllerContext context, DoubleAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "number";
    }
}