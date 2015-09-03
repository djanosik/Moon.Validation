using System.Collections.Generic;

namespace Moon.Validation.Operators
{
    /// <summary>
    /// The Not Equal (!=) operator.
    /// </summary>
    public class NotEqual : IOperator
    {
        /// <summary>
        /// Gets the default error message.
        /// </summary>
        public string DefaultErrorMessage
            => "The field {0} must not be equal to the field {1}.";

        /// <summary>
        /// Gets the name of the operator.
        /// </summary>
        public string Name
            => "NotEqual";

        /// <summary>
        /// Compares the two objects and returns whether they are equal.
        /// </summary>
        /// <param name="first">The first object.</param>
        /// <param name="second">The second object.</param>
        public bool Compare(object first, object second)
        {
            if (first == null && second == null)
            {
                return false;
            }

            if (first == null || second == null)
            {
                return true;
            }

            return Comparer<object>.Default.Compare(first, second) != 0;
        }
    }
}