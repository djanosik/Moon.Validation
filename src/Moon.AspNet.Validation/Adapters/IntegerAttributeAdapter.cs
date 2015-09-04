using Moon.Validation;

namespace Moon.AspNet.Validation
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
        public IntegerAttributeAdapter(IntegerAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "integer";
    }
}