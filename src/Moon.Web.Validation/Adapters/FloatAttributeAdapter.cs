using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="FloatAttribute" /> attribute.
    /// </summary>
    public class FloatAttributeAdapter : AttributeAdapter<FloatAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatAttributeAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        public FloatAttributeAdapter(ModelMetadata metadata, ControllerContext context, FloatAttribute attribute)
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