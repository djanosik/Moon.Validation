using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Moon.Validation;

namespace Moon.AspNet.Validation
{
    /// <summary>
    /// The convention-based client model validator provider for ASP.NET MVC.
    /// </summary>
    public class ValidationClientValidatorProvider : IClientModelValidatorProvider
    {
        readonly ITextProvider textProvider;
        readonly Dictionary<Type, Func<ValidationAttribute, IClientModelValidator>> factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationClientValidatorProvider" /> class.
        /// </summary>
        /// <param name="textProvider">The validation text provider.</param>
        public ValidationClientValidatorProvider(ITextProvider textProvider)
        {
            Requires.NotNull(textProvider, nameof(textProvider));

            factories = GetAttributeFactories();
            this.textProvider = textProvider;
        }

        /// <summary>
        /// Gets set of <see cref="IClientModelValidator" /> s by updating the
        /// <see cref="ClientValidatorProviderContext.Validators" /> collection.
        /// </summary>
        /// <param name="context">The provider context.</param>
        public void GetValidators(ClientValidatorProviderContext context)
        {
            foreach (var attribute in context.ValidatorMetadata.OfType<ValidationAttribute>())
            {
                Func<ValidationAttribute, IClientModelValidator> factory;

                if (factories.TryGetValue(attribute.GetType(), out factory))
                {
                    context.Validators.Add(factory(attribute));
                }
            }
        }

        static Dictionary<Type, Func<ValidationAttribute, IClientModelValidator>> GetAttributeFactories()
        {
            return new Dictionary<Type, Func<ValidationAttribute, IClientModelValidator>>()
            {
                [typeof(DigitsAttribute)] = attr => new DigitsAttributeAdapter((DigitsAttribute)attr),
                [typeof(DoubleAttribute)] = attr => new DoubleAttributeAdapter((DoubleAttribute)attr),
                [typeof(EqualAttribute)] = attr => new CompareAttributeAdapter<EqualAttribute>((EqualAttribute)attr),
                [typeof(FloatAttribute)] = attr => new FloatAttributeAdapter((FloatAttribute)attr),
                [typeof(GreaterThanAttribute)] = attr => new CompareAttributeAdapter<GreaterThanAttribute>((GreaterThanAttribute)attr),
                [typeof(GreaterThanOrEqualAttribute)] = attr => new CompareAttributeAdapter<GreaterThanOrEqualAttribute>((GreaterThanOrEqualAttribute)attr),
                [typeof(IntegerAttribute)] = attr => new IntegerAttributeAdapter((IntegerAttribute)attr),
                [typeof(LessThanAttribute)] = attr => new CompareAttributeAdapter<LessThanAttribute>((LessThanAttribute)attr),
                [typeof(LessThanOrEqualAttribute)] = attr => new CompareAttributeAdapter<LessThanOrEqualAttribute>((LessThanOrEqualAttribute)attr),
                [typeof(MaxAttribute)] = attr => new MaxAttributeAdapter((MaxAttribute)attr),
                [typeof(MinAttribute)] = attr => new MinAttributeAdapter((MinAttribute)attr),
                [typeof(NotEqualAttribute)] = attr => new CompareAttributeAdapter<NotEqualAttribute>((NotEqualAttribute)attr),
                [typeof(RequiredIfEmptyAttribute)] = attr => new RequiredIfEmptyAttributeAdapter((RequiredIfEmptyAttribute)attr),
                [typeof(RequiredIfEqualAttribute)] = attr => new RequiredIfAttributeAdapter<RequiredIfEqualAttribute>((RequiredIfEqualAttribute)attr),
                [typeof(RequiredIfGreaterThanAttribute)] = attr => new RequiredIfAttributeAdapter<RequiredIfGreaterThanAttribute>((RequiredIfGreaterThanAttribute)attr),
                [typeof(RequiredIfGreaterThanOrEqualAttribute)] = attr => new RequiredIfAttributeAdapter<RequiredIfGreaterThanOrEqualAttribute>((RequiredIfGreaterThanOrEqualAttribute)attr),
                [typeof(RequiredIfLessThanAttribute)] = attr => new RequiredIfAttributeAdapter<RequiredIfLessThanAttribute>((RequiredIfLessThanAttribute)attr),
                [typeof(RequiredIfLessThanOrEqualAttribute)] = attr => new RequiredIfAttributeAdapter<RequiredIfLessThanOrEqualAttribute>((RequiredIfLessThanOrEqualAttribute)attr),
                [typeof(RequiredIfNotEmptyAttribute)] = attr => new RequiredIfNotEmptyAttributeAdapter((RequiredIfNotEmptyAttribute)attr),
                [typeof(RequiredIfNotEqualAttribute)] = attr => new RequiredIfAttributeAdapter<RequiredIfNotEqualAttribute>((RequiredIfNotEqualAttribute)attr)
            };
        }
    }
}