using Microsoft.Extensions.Localization;
using Moon.Validation;

namespace Moon.AspNetCore.Validation.Adapters
{
    /// <summary>
    /// Adapter for <see cref="RequiredEmptyAttribute" /> attribute.
    /// </summary>
    public class RequiredEmptyAttributeAdapter : AttributeAdapter<RequiredEmptyAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredEmptyAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        public RequiredEmptyAttributeAdapter(RequiredEmptyAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "requiredempty";
    }
}