using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validator type.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator.</typeparam>
    public abstract class ValidatorAdapter<TValidator> : DataAnnotationsModelValidator<TValidator>
        where TValidator : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorAdapter{TValidator}" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        protected ValidatorAdapter(ModelMetadata metadata, ControllerContext context, TValidator validator)
            : base(metadata, context, validator)
        {
        }

        /// <summary>
        /// Gets the type of client validation. The default value is null which means client
        /// validation is not supported.
        /// </summary>
        protected virtual string ClientValidationType
            => null;

        /// <summary>
        /// Returns an enumeration of client validation rules.
        /// </summary>
        public override sealed IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (ClientValidationType == null)
            {
                yield break;
            }

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = ClientValidationType
            };

            foreach (var param in GetClientValidationParameters())
            {
                rule.ValidationParameters.Add(param);
            }

            yield return rule;
        }

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected virtual IDictionary<string, object> GetClientValidationParameters()
            => new Dictionary<string, object>();
    }
}