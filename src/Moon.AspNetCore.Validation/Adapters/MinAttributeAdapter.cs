using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using Moon.Validation;

namespace Moon.AspNetCore.Validation.Adapters
{
    /// <summary>
    /// Adapter for <see cref="MinAttribute" /> attribute.
    /// </summary>
    public class MinAttributeAdapter : AttributeAdapter<MinAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        public MinAttributeAdapter(MinAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "range";

        /// <summary>
        /// Gets the client validation error message.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
            => GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName(), Attribute.MinValue);

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("min", Attribute.MinValue);
            return parameters;
        }
    }
}