using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Helper used to register validator adapters.
    /// </summary>
    public static class AttributeAdapters
    {
        /// <summary>
        /// Registers validator adapters.
        /// </summary>
        public static void Register()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DigitsAttribute), typeof(DigitsAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DoubleAttribute), typeof(DoubleAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EqualAttribute), typeof(CompareAttributeAdapter<EqualAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(FloatAttribute), typeof(FloatAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(GreaterThanAttribute), typeof(CompareAttributeAdapter<GreaterThanAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(GreaterThanOrEqualAttribute), typeof(CompareAttributeAdapter<GreaterThanOrEqualAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(IntegerAttribute), typeof(IntegerAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LessThanAttribute), typeof(CompareAttributeAdapter<LessThanAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LessThanOrEqualAttribute), typeof(CompareAttributeAdapter<LessThanOrEqualAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MaxAttribute), typeof(MaxAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MinAttribute), typeof(MinAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NotEqualAttribute), typeof(CompareAttributeAdapter<NotEqualAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfEmptyAttribute), typeof(RequiredIfEmptyAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfEqualAttribute), typeof(RequiredIfAttributeAdapter<RequiredIfEqualAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfGreaterThanAttribute), typeof(RequiredIfAttributeAdapter<RequiredIfGreaterThanAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfGreaterThanOrEqualAttribute), typeof(RequiredIfAttributeAdapter<RequiredIfGreaterThanOrEqualAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfLessThanAttribute), typeof(RequiredIfAttributeAdapter<RequiredIfLessThanAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfLessThanOrEqualAttribute), typeof(RequiredIfAttributeAdapter<RequiredIfLessThanOrEqualAttribute>));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfNotEmptyAttribute), typeof(RequiredIfNotEmptyAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfNotEqualAttribute), typeof(RequiredIfAttributeAdapter<RequiredIfNotEqualAttribute>));
        }
    }
}