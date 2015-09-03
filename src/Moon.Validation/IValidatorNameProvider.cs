namespace Moon.Validation
{
    /// <summary>
    /// An interface for attributes providing validator name.
    /// </summary>
    public interface IValidatorNameProvider
    {
        /// <summary>
        /// Returns the name of the validator.
        /// </summary>
        string GetValidatorName();
    }
}