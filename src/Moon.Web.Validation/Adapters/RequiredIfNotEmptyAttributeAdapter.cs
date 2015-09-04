using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="RequiredIfNotEmptyAttribute" /> attribute.
    /// </summary>
    public class RequiredIfNotEmptyAttributeAdapter : DependentAttributeAdapter<RequiredIfNotEmptyAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfNotEmptyAttributeAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        public RequiredIfNotEmptyAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredIfNotEmptyAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "requiredifnotempty";
    }
}