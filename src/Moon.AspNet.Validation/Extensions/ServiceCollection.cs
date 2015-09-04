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
        /// <param name="services">The service collection.</param>
        /// <param name="textProvider">The validation text provider.</param>
        public static IServiceCollection AddValidation(this IServiceCollection services, ITextProvider textProvider)
        {
            Requires.NotNull(textProvider, nameof(textProvider));

            services.ConfigureMvcViews(o =>
            {
                o.ClientModelValidatorProviders.Add(new ValidationClientValidatorProvider(textProvider));
            });

            services.ConfigureMvc(o =>
            {
                o.ModelMetadataDetailsProviders.Add(new ValidationMetadataProvider(textProvider));
            });

            return services;
        }
    }
}