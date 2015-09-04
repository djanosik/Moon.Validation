using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validation attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the validation attribute.</typeparam>
    public abstract class AttributeAdapter<TAttribute> : DataAnnotationsModelValidator<TAttribute>
        where TAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeAdapter{TAttribute}" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute.</param>
        protected AttributeAdapter(ModelMetadata metadata, ControllerContext context, TAttribute attribute)
            : base(metadata, context, attribute)
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