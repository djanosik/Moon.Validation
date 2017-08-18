using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Moon.AspNetCore.Validation.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services
                .AddLocalization(o => o.ResourcesPath = "Resources")
                .AddMvc()
                .AddDataAnnotationsLocalization()
                .AddMoonValidation();

        public void Configure(IApplicationBuilder app)
            => app
                .UseRequestLocalization(new RequestLocalizationOptions {
                    DefaultRequestCulture = new RequestCulture("en"),
                    SupportedCultures = new[] {
                        new CultureInfo("en"),
                        new CultureInfo("cs")
                    },
                    SupportedUICultures = new[] {
                        new CultureInfo("en"),
                        new CultureInfo("cs")
                    }
                })
                .UseMvc();
    }
}