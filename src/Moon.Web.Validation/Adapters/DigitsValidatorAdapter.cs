using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="Digits" /> validator.
    /// </summary>
    public class DigitsValidatorAdapter : ValidatorAdapter<Digits>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitsValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public DigitsValidatorAdapter(ModelMetadata metadata, ControllerContext context, Digits validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "digits";
    }
}