using Microsoft.Extensions.Localization;
using Moon.Validation;

namespace Moon.AspNetCore.Validation.Adapters
{
    /// <summary>
    /// Adapter for <see cref="IntegerAttribute" /> attribute.
    /// </summary>
    public class IntegerAttributeAdapter : AttributeAdapter<IntegerAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        public IntegerAttributeAdapter(IntegerAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "integer";
    }
}