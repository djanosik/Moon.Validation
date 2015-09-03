using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Adapter for <see cref="Integer" /> validator.
    /// </summary>
    public class IntegerValidatorAdapter : ValidatorAdapter<Integer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerValidatorAdapter" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public IntegerValidatorAdapter(ModelMetadata metadata, ControllerContext context, Integer validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "integer";
    }
}