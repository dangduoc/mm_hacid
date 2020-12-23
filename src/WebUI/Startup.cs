using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BaseProjectWebRazor.API.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Infrastructure;
using SixLabors.ImageSharp.Web.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;
using BaseProjectWebRazor.Areas.Admin.Profiles;
using BaseProjectWebRazor.Services;

namespace BaseProjectWebRazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient()
              .AddHttpContextAccessor();
            services.SetupLocalization();
            services.AddInfrastructure(Configuration);
            services.InjectApplicationServices();

            services.AddScoped<CompanySettings>();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            //  services.InjectAppConfig(Configuration);
            services.AddAutoMapper(typeof(AdminAutoMapperProfiles));
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Admin", "/Product");
                options.Conventions.AuthorizeAreaPage("Admin", "/Index");
                //options.Conventions.AddAreaPageRoute("Admin", "/Areas/Admin/Pages", "admin/{controller=Default}/{action=Index}/{id?}");
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddControllers().AddNewtonsoftJson((MvcNewtonsoftJsonOptions options) =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            });
            //services.AddImageSharpWeb();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions =>
            {
                cookieOptions.LoginPath = "/admin/login";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
            }
           // app.UseImageSharp();

            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseRouting();
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
    
                //  endpoints.
                endpoints.MapAreaControllerRoute(name: "Admin", "Admin", pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
            });
        }
    }
}
