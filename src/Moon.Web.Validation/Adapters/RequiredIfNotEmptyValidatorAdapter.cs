using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="RequiredIfNotEmpty" /> validator.
    /// </summary>
    public class RequiredIfNotEmptyValidatorAdapter : DependentValidatorAdapter<RequiredIfNotEmpty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfNotEmptyValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public RequiredIfNotEmptyValidatorAdapter(ModelMetadata metadata, ControllerContext context, RequiredIfNotEmpty validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "requiredifnotempty";
    }
}