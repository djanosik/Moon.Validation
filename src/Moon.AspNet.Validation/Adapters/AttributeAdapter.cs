using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;

namespace Moon.AspNet.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validation attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the validation attribute.</typeparam>
    public abstract class AttributeAdapter<TAttribute> : DataAnnotationsClientModelValidator<TAttribute>
        where TAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeAdapter{TAttribute}" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        protected AttributeAdapter(TAttribute attribute)
            : base(attribute, null)
        {
        }

        /// <summary>
        /// Gets the type of client validation. The default value is null which means client
        /// validation is not supported.
        /// </summary>
        protected virtual string ValidationType
            => null;

        /// <summary>
        /// Returns an enumeration of client validation rules.
        /// </summary>
        /// <param name="context">The validation context.</param>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ClientModelValidationContext context)
        {
            if (ValidationType == null)
            {
                yield break;
            }
            
            var rule = new ModelClientValidationRule(ValidationType, GetErrorMessage(context.ModelMetadata));

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