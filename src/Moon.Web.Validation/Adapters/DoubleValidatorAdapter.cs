using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="Double" /> validator.
    /// </summary>
    public class DoubleValidatorAdapter : ValidatorAdapter<Double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public DoubleValidatorAdapter(ModelMetadata metadata, ControllerContext context, Double validator)
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