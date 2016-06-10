using System.Linq;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Moon;
using Moon.AspNetCore.Validation;
using Moon.Validation;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IMvcBuilder" /> extension methods.
    /// </summary>
    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// Adds services required to use localizable validation attributes.
        /// </summary>
        /// <param name="builder">The ASP.NET MVC builder.</param>
        /// <param name="textProvider">The validation text provider.</param>
        public static IMvcBuilder AddValidation(this IMvcBuilder builder, ITextProvider textProvider)
        {
            Requires.NotNull(textProvider, nameof(textProvider));

            builder.AddViewOptions(o =>
            {
                var numericProvider = o.ClientModelValidatorProviders
                    .OfType<NumericClientModelValidatorProvider>()
                    .FirstOrDefault();

                o.ClientModelValidatorProviders.Add(new ValidationClientValidatorProvider(textProvider));

                if (numericProvider != null)
                {
                    // it would conflict with our client-side rules for Double and Float attributes
                    o.ClientModelValidatorProviders.Remove(numericProvider);
                }
            });

            builder.AddMvcOptions(o =>
            {
                o.ModelMetadataDetailsProviders.Add(new ValidationMetadataProvider(textProvider));
            });

            return builder;
        }
    }
}