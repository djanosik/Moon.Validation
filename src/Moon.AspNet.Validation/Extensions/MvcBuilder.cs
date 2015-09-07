using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Moon;
using Moon.AspNet.Validation;
using Moon.Validation;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection" /> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services required to use localizable validation attributes.
        /// </summary>
        /// <param name="services">The ASP.NET MVC builder.</param>
        /// <param name="textProvider">The validation text provider.</param>
        public static IMvcBuilder AddValidation(this IMvcBuilder builder, ITextProvider textProvider)
        {
            Requires.NotNull(textProvider, nameof(textProvider));

            var services = builder.Services;

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

            services.Configure<MvcOptions>(o =>
            {
                o.ModelMetadataDetailsProviders.Add(new ValidationMetadataProvider(textProvider));
            });

            return builder;
        }
    }
}