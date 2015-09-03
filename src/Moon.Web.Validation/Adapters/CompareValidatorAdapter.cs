using System.Collections.Generic;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validator type.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator.</typeparam>
    public class CompareValidatorAdapter<TValidator> : DependentValidatorAdapter<TValidator>
        where TValidator : CompareValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValidatorAdapter{TValidator}" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        public CompareValidatorAdapter(ModelMetadata metadata, ControllerContext context, TValidator validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ClientValidationType
            => "compare";

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("operator", Attribute.Operator.Name);
            return parameters;
        }
    }
}