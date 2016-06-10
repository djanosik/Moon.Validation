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
        public FloatAttributeAdapter(FloatAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "number";
    }
}