using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Moon.AspNetCore.Validation;

// ReSharper disable once CheckNamespace

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
        public static IMvcBuilder AddMoonValidation(this IMvcBuilder builder)
        {
            AddMoonValidationServices(builder.Services);
            return builder;
        }

        private static void AddMoonValidationServices(IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MoonValidationMvcOptionsSetup>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcViewOptions>, MoonValidationMvcViewOptionsSetup>());
            services.Replace(ServiceDescriptor.Singleton<IValidationAttributeAdapterProvider, MoonValidationAttributeAdapterProvider>());
        }
    }
}