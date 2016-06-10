using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Adapter for <see cref="DigitsAttribute" /> attribute.
    /// </summary>
    public class DigitsAttributeAdapter : AttributeAdapter<DigitsAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitsAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        public DigitsAttributeAdapter(DigitsAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "digits";
    }
}