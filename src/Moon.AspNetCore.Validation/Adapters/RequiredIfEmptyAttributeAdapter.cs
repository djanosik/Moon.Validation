using Microsoft.Extensions.Localization;
using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Adapter for <see cref="RequiredIfEmptyAttribute" /> attribute.
    /// </summary>
    public class RequiredIfEmptyAttributeAdapter : DependentAttributeAdapter<RequiredIfEmptyAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfEmptyAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        public RequiredIfEmptyAttributeAdapter(RequiredIfEmptyAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "requiredifempty";
    }
}