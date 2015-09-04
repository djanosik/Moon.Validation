using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="RequiredIfEmptyAttribute" /> attribute.
    /// </summary>
    public class RequiredIfEmptyAttributeAdapter : DependentAttributeAdapter<RequiredIfEmptyAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfEmptyAttributeAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        public RequiredIfEmptyAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredIfEmptyAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "requiredifempty";
    }
}