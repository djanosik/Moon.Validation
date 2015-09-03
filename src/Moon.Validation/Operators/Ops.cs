namespace Moon.Validation.Operators
{
    /// <summary>
    /// Provides instances of existing operators.
    /// </summary>
    public static class Ops
    {
        /// <summary>
        /// Initializes the <see cref="Ops" /> class.
        /// </summary>
        static Ops()
        {
            Equal = new Equal();
            GreaterThan = new GreaterThan();
            GreaterThanOrEqual = new GreaterThanOrEqual();
            LessThan = new LessThan();
            LessThanOrEqual = new LessThanOrEqual();
            NotEqual = new NotEqual();
        }

        /// <summary>
        /// Gets the Equal operator.
        /// </summary>
        public static Equal Equal { get; }

        /// <summary>
        /// Gets the GreaterThan operator.
        /// </summary>
        public static GreaterThan GreaterThan { get; }

        /// <summary>
        /// Gets the GreaterThanOrEqual operator.
        /// </summary>
        public static GreaterThanOrEqual GreaterThanOrEqual { get; }

        /// <summary>
        /// Gets the LessThan operator.
        /// </summary>
        public static LessThan LessThan { get; }

        /// <summary>
        /// Gets the LessThanOrEqual operator.
        /// </summary>
        public static LessThanOrEqual LessThanOrEqual { get; }

        /// <summary>
        /// Gets the NotEqual operator.
        /// </summary>
        public static NotEqual NotEqual { get; }
    }
}