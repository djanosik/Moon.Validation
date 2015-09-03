namespace Moon.Validation.Operators
{
    /// <summary>
    /// The comparison operator.
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// Gets the default error message.
        /// </summary>
        string DefaultErrorMessage { get; }

        /// <summary>
        /// Gets the name of the operator.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Compares the two objects and returns whether they meet a condition represented by the operator.
        /// </summary>
        /// <param name="first">The first object.</param>
        /// <param name="second">The second object.</param>
        bool Compare(object first, object second);
    }
}