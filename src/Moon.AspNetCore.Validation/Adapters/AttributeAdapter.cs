using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace Moon.AspNetCore.Validation.Adapters
{
    /// <summary>
    /// Provides a model validator for the specified validation attribute.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the validation attribute.</typeparam>
    public abstract class AttributeAdapter<TAttribute> : AttributeAdapterBase<TAttribute>
        where TAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeAdapter{TAttribute}" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        protected AttributeAdapter(TAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// Gets the type of client validation. The default value is null which means client
        /// validation is not supported.
        /// </summary>
        protected virtual string ValidationType
            => null;

        /// <summary>
        /// Adds client validation attributes.
        /// </summary>
        /// <param name="context">The validation context.</param>
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (ValidationType == null)
            {
                return;
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, $"data-val-{ValidationType}", GetErrorMessage(context));

            foreach (var param in GetClientValidationParameters())
            {
                MergeAttribute(context.Attributes, $"data-val-{ValidationType}-{param.Key}", Convert.ToString(param.Value, CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets the client validation error message.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
            => GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected virtual IDictionary<string, object> GetClientValidationParameters()
            => new Dictionary<string, object>();
    }
}