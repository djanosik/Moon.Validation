using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moon.AspNetCore.Mvc;
using Moon.Localization;
using Moon.Localization.Validation;

namespace Moon.AspNetCore.Validation.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IRazorViewEngine, PagesViewEngine>()
                .AddMvc()
                .AddValidation(new LocalizationTextProvider());

            services.Configure<RouteOptions>(o =>
            {
                o.LowercaseUrls = true;
                o.AppendTrailingSlash = true;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseLocalization(r =>
                r.LoadJson("resources"));

            app.UseMvc();
        }
    }
}