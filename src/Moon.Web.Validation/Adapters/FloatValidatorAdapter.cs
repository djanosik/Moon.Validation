using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="Float" /> validator.
    /// </summary>
    public class FloatValidatorAdapter : ValidatorAdapter<Float>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public FloatValidatorAdapter(ModelMetadata metadata, ControllerContext context, Float validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "number";
    }
}