using Moon.Validation;

namespace Moon.AspNet.Validation
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
        public DoubleAttributeAdapter(DoubleAttribute attribute)
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