using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WValidation = System.Windows.Controls.Validation;

namespace Moon.Windows.Validation
{
    /// <summary>
    /// The WPF validation helper.
    /// </summary>
    public static class WpfValidation
    {
        static readonly MethodInfo addValidationError = typeof(WValidation).GetMethod("AddValidationError", BindingFlags.Static | BindingFlags.NonPublic);
        static readonly MethodInfo removeValidationError = typeof(WValidation).GetMethod("RemoveValidationError", BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Removes all errors from the specified element.
        /// </summary>
        /// <param name="element">The element to remove errors from.</param>
        public static void ClearInvalid(DependencyObject element)
        {
            var errorsToRemove = WValidation.GetErrors(element)
                .ToArray();

            foreach (var error in errorsToRemove)
            {
                removeValidationError.Invoke(null, new object[] { error, element, true });
            }
        }

        /// <summary>
        /// Marks the specified element as invalid with the specified error content.
        /// </summary>
        /// <param name="element">The element to be marked as invalid.</param>
        /// <param name="errorContent">
        /// An object that provides additional context for this validation error, such as a string
        /// describing the error.
        /// </param>
        public static void MarkInvalid(DependencyObject element, object errorContent)
        {
            addValidationError.Invoke(null, new object[]
            {
                new ValidationError(new ExceptionValidationRule(), new object()) { ErrorContent = errorContent },
                element, true
            });
        }
    }
}