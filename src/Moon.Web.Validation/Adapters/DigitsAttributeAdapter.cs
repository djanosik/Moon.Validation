using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="DigitsAttribute" /> attribute.
    /// </summary>
    public class DigitsAttributeAdapter : AttributeAdapter<DigitsAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitsAttributeAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        public DigitsAttributeAdapter(ModelMetadata metadata, ControllerContext context, DigitsAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "digits";
    }
}