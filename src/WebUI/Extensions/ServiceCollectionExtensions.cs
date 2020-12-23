using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization.Routing;
using BaseProjectWebRazor.Services;
using Application.Common.Interfaces;
using API.Services;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Processors;

namespace BaseProjectWebRazor.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectApplicationServices(this IServiceCollection services)
        {

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<ICurrentRequestLanguageService, CurrentRequestLanguageService>();
            services.AddSingleton<ILocalUploadService, LocalUploadService>();
            #region Handlers

            #endregion
            return services;
        }
        public static IServiceCollection SetupLocalization(this IServiceCollection services)
        {
            services.AddSingleton<CommonLocalizationService>();
            services.AddLocalization(opts => opts.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]{
                     new CultureInfo("vi"),
                    new CultureInfo("en")
                };
                options.DefaultRequestCulture = new RequestCulture("vi");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                     new RouteDataRequestCultureProvider(),
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
            return services;
        }
        public static void AddImageSharpWeb(this IServiceCollection services)
        {
            services.AddImageSharp()
               .RemoveProcessor<FormatWebProcessor>()
               .RemoveProcessor<BackgroundColorWebProcessor>();
        }
    }
}
