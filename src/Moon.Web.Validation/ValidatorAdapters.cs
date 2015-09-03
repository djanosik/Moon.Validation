using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Helper used to register validator adapters.
    /// </summary>
    public static class ValidatorAdapters
    {
        /// <summary>
        /// Registers validator adapters.
        /// </summary>
        public static void Register()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Digits), typeof(DigitsValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Double), typeof(DoubleValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Equal), typeof(CompareValidatorAdapter<Equal>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Float), typeof(FloatValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(GreaterThan), typeof(CompareValidatorAdapter<GreaterThan>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(GreaterThanOrEqual), typeof(CompareValidatorAdapter<GreaterThanOrEqual>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Integer), typeof(IntegerValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LessThan), typeof(CompareValidatorAdapter<LessThan>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LessThanOrEqual), typeof(CompareValidatorAdapter<LessThanOrEqual>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Max), typeof(MaxValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Min), typeof(MinValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NotEqual), typeof(CompareValidatorAdapter<NotEqual>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfEmpty), typeof(RequiredIfEmptyValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfEqual), typeof(RequiredIfValidatorAdapter<RequiredIfEqual>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfGreaterThan), typeof(RequiredIfValidatorAdapter<RequiredIfGreaterThan>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfGreaterThanOrEqual), typeof(RequiredIfValidatorAdapter<RequiredIfGreaterThanOrEqual>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfLessThan), typeof(RequiredIfValidatorAdapter<RequiredIfLessThan>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfLessThanOrEqual), typeof(RequiredIfValidatorAdapter<RequiredIfLessThanOrEqual>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfNotEmpty), typeof(RequiredIfNotEmptyValidatorAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfNotEqual), typeof(RequiredIfValidatorAdapter<RequiredIfNotEqual>));
        }
    }
}