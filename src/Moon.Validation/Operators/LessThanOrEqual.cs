using System.Collections.Generic;

namespace Moon.Validation.Operators
{
    /// <summary>
    /// The LessThanOrEqual (&lt;=) operator.
    /// </summary>
    public class LessThanOrEqual : IOperator
    {
        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public string DefaultErrorMessage
            => "The field {0} must be less than or equal to the field {1}.";

        /// <summary>
        /// Gets the name of the operator.
        /// </summary>
        public string Name
            => "LessThanOrEqual";

        /// <summary>
        /// Compares the two objects and returns whether the first one is less than or equal to the second.
        /// </summary>
        /// <param name="first">The first object.</param>
        /// <param name="second">The second object.</param>
        public bool Compare(object first, object second)
        {
            if (first == null || second == null)
            {
                return false;
            }

            return Comparer<object>.Default.Compare(first, second) <= 0;
        }
    }
}