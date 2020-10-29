using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder AddAntiForgery(this IApplicationBuilder app, IAntiforgery antiforgery)
        {
            app.Use(async (context, next) =>
            {
                string path = context.Request.Path.Value;
                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }
                await next.Invoke();
            });
            return app;
        }
    }
}
