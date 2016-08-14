using Microsoft.Extensions.Localization;
using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Adapter for <see cref="FloatAttribute" /> attribute.
    /// </summary>
    public class FloatAttributeAdapter : AttributeAdapter<FloatAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        public FloatAttributeAdapter(FloatAttribute attribute, IStringLocalizer stringLocalizer)
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