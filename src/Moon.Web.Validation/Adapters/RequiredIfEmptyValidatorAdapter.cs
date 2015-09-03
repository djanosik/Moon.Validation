using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="RequiredIfEmpty" /> validator.
    /// </summary>
    public class RequiredIfEmptyValidatorAdapter : DependentValidatorAdapter<RequiredIfEmpty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfEmptyValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public RequiredIfEmptyValidatorAdapter(ModelMetadata metadata, ControllerContext context, RequiredIfEmpty validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "requiredifempty";
    }
}