using Microsoft.Extensions.Localization;
using Moon.Validation;

namespace Moon.AspNetCore.Validation.Adapters
{
    /// <summary>
    /// Adapter for <see cref="DoubleAttribute" /> attribute.
    /// </summary>
    public class DoubleAttributeAdapter : AttributeAdapter<DoubleAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        public DoubleAttributeAdapter(DoubleAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "number";
    }
}